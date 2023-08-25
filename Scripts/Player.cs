using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Abilitity defaultAbility;
    public List<PlayerAbility> abilitities = new();
    public List<int> abilititiesCount = new();
    public List<float> abilititiesRegenerationState = new();


    public bool coolDown = false;
    public bool defaultCoolDown = false;
    public float coolDownTime = 0.5f;
    public float defaultCoolDownTime = 0.6f;
    [Header("Components")]
    public Movement movement;

    public bool mouseOnMap;
    public Vector3 mouseTargetPoint;

    public float speedLimit = 3f;
    public bool unlimitedSpeed = false;
    float abilititiesNumber = 3;


    public Color slowDownColor;
    public List<float> slowDonws = new();
    SpriteRenderer sr;
    private void Awake()
    {
        abilitities.Clear();
        for (int i = 0; i < abilititiesNumber; i++)
        {
            if (PlayerPrefs.HasKey("P|" + i))
            {
                int AbilitityIndex = PlayerPrefs.GetInt("P|" + i);
                if (AbilitityIndex != -1)
                {
                    abilitities.Add(LevelManager.get.playerAbilities[AbilitityIndex]);
                }
                else
                {
                    abilitities.Add(null);
                }
            }
            else
            {
                abilitities.Add(null);
            }
        }
        
        abilititiesCount.Clear();
        abilititiesRegenerationState.Clear();
        foreach(PlayerAbility ability in abilitities)
        {
            if(ability != null)
                abilititiesCount.Add(ability.count);
            else
                abilititiesCount.Add(0);
            abilititiesRegenerationState.Add(0);
        }
        StartCoroutine(abilitityTime());
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if(slowDonws.Count > 0)
        {
            sr.color = slowDownColor;
        }
        else
        {
            sr.color = Color.white;
        }
        if(!defaultCoolDown && Input.GetMouseButtonDown(0))
        {
            if (TutorialManager.get != null && TutorialManager.get.tutorialIndex == 7)
            {
                TutorialManager.get.changeTutorialStep();
            }
            Manager.get.UseAbilititiy(gameObject, defaultAbility);
            DefaultCoolDown();
        }
        if (!coolDown)
        {
            if (Input.GetKeyDown("q") && abilititiesCount[0] > 0 && Manager.get.canPlayerUseAbilititiy(abilitities[0].Abilitity))
            {
                abilititiesCount[0]--;
                Manager.get.UseAbilititiy(gameObject, abilitities[0].Abilitity);
                CoolDown();
                if (TutorialManager.get != null && TutorialManager.get.tutorialIndex == 8)
                {
                    TutorialManager.get.changeTutorialStep();
                }
            }
            if (Input.GetKeyDown("e") && abilititiesCount[1] > 0 && Manager.get.canPlayerUseAbilititiy(abilitities[1].Abilitity))
            {
                abilititiesCount[1]--;
                Manager.get.UseAbilititiy(gameObject, abilitities[1].Abilitity);
                CoolDown();
                if (TutorialManager.get != null && TutorialManager.get.tutorialIndex == 8)
                {
                    TutorialManager.get.changeTutorialStep();
                }

            }
            if (Input.GetKeyDown("c") && abilititiesCount[2] > 0 && Manager.get.canPlayerUseAbilititiy(abilitities[2].Abilitity))
            {
                abilititiesCount[2]--;
                Manager.get.UseAbilititiy(gameObject, abilitities[2].Abilitity);
                CoolDown();
                if (TutorialManager.get != null && TutorialManager.get.tutorialIndex == 8)
                {
                    TutorialManager.get.changeTutorialStep();
                }
            }
        }
        
        
        Vector3 worldTargetPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,0.01f));
        //mouseTargetPoint = Input.mousePosition;
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position,(worldTargetPoint - Camera.main.transform.position),out hit,1000,~LayerMask.NameToLayer("Default")))
        {
            mouseTargetPoint = hit.point;
            mouseOnMap = true;
        }
        else
        {
            mouseTargetPoint = worldTargetPoint;
            mouseOnMap = false;
        }
    }
    IEnumerator abilitityTime()
    {
        float timeStamp = 0.1f;
        float multiplayer = 1;
        if(Time.timeScale > 1)
        {
            multiplayer *= Time.timeScale * 1.5f;
            
        }
        for (int i = 0;i < abilitities.Count;i++)
        {
            if (abilitities[i] != null && abilitities[i].count > abilititiesCount[i] && abilitities[i].Regeneration != null && abilitities[i].Regeneration.type == AbilititiRegenerationType.time)
            {
                abilititiesRegenerationState[i] += timeStamp * multiplayer;
                if (abilititiesRegenerationState[i] >= abilitities[i].Regeneration.value)
                {
                    abilititiesRegenerationState[i] = 0;
                    if(abilitities[i].Regeneration.count == AbilititiRegenerationCount.one)
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
        Manager.get.updatePlayerAUI();
        yield return new WaitForSeconds(timeStamp);
        StartCoroutine(abilitityTime());
    }
    public void AddPoints(float value)
    {
        for (int i = 0; i < abilitities.Count; i++)
        {
            if (abilitities[i] != null && abilitities[i].count > abilititiesCount[i] && abilitities[i].Regeneration != null && abilitities[i].Regeneration.type == AbilititiRegenerationType.points)
            {
                float abilityPoints = value;
                while(abilityPoints + abilititiesRegenerationState[i] >= abilitities[i].Regeneration.value && abilitities[i].count > abilititiesCount[i])
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
    public void AddKills(float value = 1)
    {
        for (int i = 0; i < abilitities.Count; i++)
        {
            if (abilitities[i] != null &&  abilitities[i].count > abilititiesCount[i] && abilitities[i].Regeneration != null && abilitities[i].Regeneration.type == AbilititiRegenerationType.kills)
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
    public void unlimitSpeed(float time, float speedlimit)
    {
        unlimitedSpeed = true;
        speedLimit = speedlimit;
        Invoke("limitSpeed", time);
    }
    void limitSpeed()
    {
        unlimitedSpeed = false;
    }
    public void CoolDown()
    {
        CancelInvoke("CancelCoolDown");
        coolDown = true;
        Invoke("CancelCoolDown", coolDownTime);
    }
    public void DefaultCoolDown()
    {
        CancelInvoke("CancelDefaultCoolDown");
        defaultCoolDown = true;
        float multiplayer = 1;
        if(Time.timeScale < 1)
        {
            multiplayer = 0.7f;
        }
        Invoke("CancelDefaultCoolDown", defaultCoolDownTime * multiplayer);
    }
    void CancelCoolDown()
    {
        coolDown = false;
    }
    void CancelDefaultCoolDown()
    {
        defaultCoolDown = false;
    }
    public void AddSlowDown(float multiplayer, float time)
    {
        StartCoroutine(addSlowdown(multiplayer, time));
    }
    IEnumerator addSlowdown(float multiplayer,float time)
    {
        slowDonws.Add(multiplayer);
        yield return new WaitForSeconds(time);
        slowDonws.Remove(multiplayer);
    }
}
