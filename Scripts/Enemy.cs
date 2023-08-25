using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float hp;
    public float points;
    public List<EnemyAbilitities> abilitities;
    public List<int> abilititiesCount = new();
    public List<float> abilititiesRegenerationState = new();


    public EnemyAbilitities selectedAbility;

    Abilitity lastUsed;
    public bool lastAbilityHited = false;
    float maxhp;
    bool theSameAvability = false;
    EnemyCanvas canvas;
    [Header("stats")]
    public float TimeToStopAttack;
    public float distanceToStop;
    public float seeRange = 20;
    public float speed;
    public List<float> slowDonws = new();
    [Header("Info")]
    public bool seePlayer;
    public bool isAttack;
    public bool coolDown = false;
    public GameObject seenObject;
    Vector3 rayHitPoint;
    NavMeshAgent agent;

    [Space(30)]
    public SpriteRenderer Renderer;

    public Sprite idle;
    public Sprite moving1;
    public Sprite moving2;

    public bool isMoving;

    public float animationState;
    float animationTimeStamp = 0.6f;

    bool animationLoopActive = false;
    private void Awake()
    {
        abilititiesCount.Clear();
        abilititiesRegenerationState.Clear();
        foreach (EnemyAbilitities ability in abilitities)
        {
            abilititiesCount.Add(ability.count);
            abilititiesRegenerationState.Add(0);
        }
        StartCoroutine(abilitityTime());
        StartCoroutine(selectAvability());
        
    }
    void Start()
    {
        maxhp = hp;
        canvas = GetComponentInChildren<EnemyCanvas>();
        agent = GetComponent<NavMeshAgent>();
        UpdateCanvas();
        isMoving = false;
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Manager.PlayerPosition - transform.position, out hit, seeRange, LayerMask.GetMask("Default")))
        {
            rayHitPoint = hit.point;
            seenObject = hit.transform.gameObject;
            if (Manager.isPlayer(hit.transform.gameObject))
            {
                seePlayer = true;
                Attack();
                CancelInvoke("Unattack");
                if (!animationLoopActive)
                {
                    StartCoroutine(changeAnimation());
                }
            }
            else
            {
                seePlayer = false;
                invokeUnAttack();
            }
        }
        else
        {
            seePlayer = false;
            invokeUnAttack();
        }
        if (isAttack)
        {
            if(seePlayer)
            {
                if (Vector3.Distance(transform.position, Manager.PlayerPosition) > distanceToStop)
                {
                    agent.SetDestination(Manager.PlayerPosition);
                    isMoving = true;
                    if (!animationLoopActive)
                    {
                        StartCoroutine(changeAnimation());
                    }
                    agent.speed = speed;
                }
                else
                {
                    isMoving = false;
                    agent.speed = 0;
                }
            }
            else
            {
                agent.SetDestination(Manager.PlayerPosition);
                isMoving = true;
                if(!animationLoopActive)
                {
                    StartCoroutine(changeAnimation());
                }
                agent.speed = speed;
            }
            if(!coolDown)
            {
                SelectAvability();
                if (selectedAbility != null)
                    StartCoroutine(useAvabilitity());
            }
        }
        else
        {
            isMoving = false;
        }

        canvas.gameObject.SetActive(isAttack || (hp != maxhp));
    }
    void Attack()
    {
        if (isAttack) return;
        isAttack = true;
        StartCoroutine(selectAvability());
    }
    public void addHealth(float value)
    {
        hp = Mathf.Clamp(hp+value,0,maxhp);
        UpdateCanvas();
    }
    public void takeHealth(float value)
    {
        
        Manager.AddPoints(Mathf.Clamp(value,0,hp) /maxhp * points);
        hp -= Mathf.Abs(value);
        if (hp <= 0)
        {
            Manager.AddKills();
            Destroy(gameObject);
        }
        UpdateCanvas();
    }
    void UpdateCanvas()
    {
        if (canvas == null) return;
        canvas.healthBar.fillAmount = hp / maxhp;
    }
    IEnumerator useAvabilitity()
    {
        
        if (selectedAbility != null && selectedAbility.abilitity != null && !coolDown)
        {
            EnemyAbilitities usedAbility = selectedAbility;
            coolDown = true;
            abilititiesCount[abilitities.IndexOf(usedAbility)]--;
            if(!theSameAvability)
            {
                yield return new WaitForSeconds(usedAbility.delay);
            }
            
            lastUsed = usedAbility.abilitity;
            theSameAvability = true;
            Manager.get.UseAbilititiy(gameObject, usedAbility.abilitity);
            CancelInvoke("CoolDownEnd");
            Invoke("CoolDownEnd", usedAbility.cooldown);
        }
    }
    IEnumerator changeAnimation()
    {
        if(isMoving)
        {
            if(animationState == 0)
            {
                Renderer.sprite = moving1;
                animationState = 1;
            }
            else
            {
                Renderer.sprite = moving2;
                animationState = 0;

            }
            animationLoopActive = true;
            yield return new WaitForSeconds(animationTimeStamp);
            StartCoroutine(changeAnimation());
        }
        else
        {
            Renderer.sprite = idle;
            animationLoopActive = false;
        }
    }
    void CoolDownEnd()
    {
        coolDown = false;
    }
    IEnumerator selectAvability()
    {
        SelectAvability();
        bool loop = isAttack;
        yield return new WaitForSeconds(0.2f);
        if(loop)
            StartCoroutine(selectAvability());
    }
    bool canUseAbilitity(EnemyScheme scheme)
    {
        if (scheme != null)
        {
            if (scheme.distance)
            {
                float distance = Vector3.Distance(transform.position, Manager.PlayerPosition);
                if (!(scheme.minDistance <= distance && distance <= scheme.maxDistance)) return false;
            }
            if (scheme.requiredHealth && !(scheme.minHp <= hp && hp <= scheme.maxHp)) return false;
            if (scheme.requireSeePlayer && !seePlayer) return false;
            if (scheme.requireSeeNotPlayer && seePlayer) return false;
            if (scheme.requiredPlayerHealth && !(scheme.minPlayerHp < Manager.Health && Manager.Health < scheme.maxPlayerHp)) return false;
            if (scheme.afterUse && lastUsed != scheme.usedAbility) return false;
            if (scheme.afterHit && !(lastUsed == scheme.usedAbility && lastAbilityHited)) return false;
        }

        return true;
    }
    IEnumerator abilitityTime()
    {
        float timeStamp = 0.2f;

        for (int i = 0; i < abilitities.Count; i++)
        {
            if (abilitities[i].count > abilititiesCount[i] && abilitities[i].Regeneration != null && abilitities[i].Regeneration.type == AbilititiRegenerationType.time)
            {
                abilititiesRegenerationState[i] += timeStamp;
                if (abilititiesRegenerationState[i] >= abilitities[i].Regeneration.value)
                {
                    abilititiesRegenerationState[i] = 0;
                    if (abilitities[i].Regeneration.count == AbilititiRegenerationCount.one)
                    {
                        abilititiesCount[i]++;
                    }
                    else
                    {
                        abilititiesCount[i] = abilitities[i].count;
                    }

                }
            }
        }
        yield return new WaitForSeconds(timeStamp);
        StartCoroutine(abilitityTime());
    }
    void SelectAvability()
    {
        if (!coolDown)
        {

            selectedAbility = null;

            int index = 0;
            foreach (EnemyAbilitities abilitity in abilitities)
            {

                EnemyScheme scheme = abilitity.enemyScheme;
                if (canUseAbilitity(scheme) && abilititiesCount[index] > 0)
                {
                    selectedAbility = abilitity;
                    break;
                }
                index++;
            }
            if (selectedAbility == null || selectedAbility != lastUsed)
            {
                theSameAvability = false;
            }
        }
    }
    public void AddPoints(float value)
    {
        for (int i = 0; i < abilitities.Count; i++)
        {
            if (abilitities[i].count > abilititiesCount[i] && abilitities[i].Regeneration != null && abilitities[i].Regeneration.type == AbilititiRegenerationType.points)
            {
                float abilityPoints = value;
                while (abilityPoints + abilititiesRegenerationState[i] >= abilitities[i].Regeneration.value && abilitities[i].count > abilititiesCount[i])
                {
                    abilititiesCount[i]++;
                    abilityPoints -= (abilitities[i].Regeneration.value - abilititiesRegenerationState[i]);
                    abilititiesRegenerationState[i] = 0;

                }

                if (abilitities[i].count <= abilititiesCount[i])
                {
                    abilititiesRegenerationState[i] = 0;
                }
                else
                {
                    abilititiesRegenerationState[i] += abilityPoints;
                }
            }
        }
    }
    void invokeUnAttack()
    {
        if (!IsInvoking("Unattack")) Invoke("Unattack", TimeToStopAttack);

    }
    void Unattack()
    {
        isAttack = false;
    }
    public void AddSlowDown(float multiplayer, float time)
    {
        StartCoroutine(addSlowdown(multiplayer, time));
    }
    IEnumerator addSlowdown(float multiplayer, float time)
    {
        slowDonws.Add(multiplayer);
        yield return new WaitForSeconds(time);
        slowDonws.Remove(multiplayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, rayHitPoint);
    }
}