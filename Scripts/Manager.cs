using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public float health;
    public float maxHealth;


    public float timeRegenerationSpeed;
    public float timeUseSpeed;
    public float timeCount;
    public float maxTimeCount;

    public float targetTimeScale;
    public float TimeScaleAnimationStart;


    public float throwAngle = 70;
    [Header("UI")]
    public Image healthBarImage;
    public TextMeshProUGUI healthText;
    public AbilititiTile[] abilititiBars;
    public Image timeBarImage;
    public Image timeBarImage2;
    public GameObject menuObject;

    public RectTransform showNewAvabilityObject;
    float displayedHealth = 100;


    bool isMenuOpened = false;
    float timeAnimationEnd;
    public bool showNewAvability;
    public float showNewAvabilityTime = 5f;
    public float showNewAvabilityFadeTime = 1f;

    public Vector2 showNewAvabilityPlace;

    public float startSceneTime;

    public TextMeshProUGUI enemysCount;
    public Image enemysCountbar;
    PlayerCamera pcamera;

    float startEnemysCount;
    public static float Health
    {
        get { return Instance.health; }
        set { Instance.health = value; }
    }
    public static GameObject Player 
    {
        get
        {
            if(get.player != null)
                return get.player;
            else return FindObjectOfType<Player>().gameObject;
        }
    }
    public static Player PlayerComponent
    {
        get
        {
            if (get.player != null)
                return get.playerComponent;
            else return FindObjectOfType<Player>();
        }

    }
    GameObject player;
    Player playerComponent;
    public static Manager get
    {
        get
        {
            if (Instance != null)
                return Instance;
            else
                return FindObjectOfType<Manager>();
        }
    }
    public static Vector3 PlayerPosition
    {
        get
        {
            return Player.transform.position;
        }
        set
        {
            Player.transform.position = value;
        }
    }
   
    void Awake()
    {
        displayedHealth = health;
        Instance = this;
        playerComponent = FindObjectOfType<Player>();
        player = playerComponent.gameObject;
        updateCanvas();
        startSceneTime = Time.time;
        foreach(SpriteRenderer  sr in FindObjectsOfType<SpriteRenderer>())
        {
            sr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

        }
        pcamera = FindAnyObjectByType<PlayerCamera>();
    }
    private void Start()
    {
        updatePlayerAUI();

        showNewAvabilityObject.gameObject.SetActive(showNewAvability);
        startEnemysCount = FindObjectsOfType<Enemy>().Length;
    }

    void Update()
    {
        displayedHealth = Mathf.Lerp(displayedHealth, health, 40f * Time.deltaTime / Time.timeScale);
        healthText.text = Mathf.Round(displayedHealth).ToString();
        healthBarImage.fillAmount = Mathf.Lerp(healthBarImage.fillAmount, health / maxHealth, 5f * Time.deltaTime / Time.timeScale);
        if (targetTimeScale == 1)
        {
            timeCount = Mathf.Clamp(timeCount + (timeRegenerationSpeed * Time.deltaTime), 0, maxTimeCount);
            //timeBarImage.transform.parent.gameObject.SetActive(false);
        }
        else if(!isMenuOpened)
        {
            timeCount = Mathf.Clamp(timeCount - (timeUseSpeed * Time.deltaTime / Time.timeScale), 0, maxTimeCount);

            timeBarImage.transform.parent.gameObject.SetActive(true);
            //timeBarImage.fillAmount = timeCount / maxTimeCount;
        }
        timeBarImage2.fillAmount = timeCount / maxTimeCount;
        float timeScaleAnimationLenght = 4f;
        float requiredTime = 0.2f;
        float slowScale = 0.3f;
        float speedScale = 2;
        if (!isMenuOpened)
        {
            if (timeCount > 0 && ((targetTimeScale == speedScale &&  Input.GetKey(KeyCode.LeftShift)) || (targetTimeScale != speedScale && Input.GetKeyDown(KeyCode.LeftShift) && timeCount / maxTimeCount > requiredTime)))
            {
                targetTimeScale = speedScale;
                TimeScaleAnimationStart = Time.time;
                if (TutorialManager.get != null && TutorialManager.get.tutorialIndex == 9)
                {
                    TutorialManager.get.changeTutorialStep();
                }
            }
            else if (timeCount > 0 && ((targetTimeScale == slowScale && Input.GetKey(KeyCode.LeftControl)) || (targetTimeScale != slowScale && Input.GetKeyDown(KeyCode.LeftControl) && timeCount / maxTimeCount > requiredTime)))
            {
                targetTimeScale = slowScale;
                TimeScaleAnimationStart = Time.time;
                if (TutorialManager.get != null && TutorialManager.get.tutorialIndex == 10)
                {
                    TutorialManager.get.changeTutorialStep();
                }
            }
            else if (targetTimeScale != 1)
            {
                targetTimeScale = 1;
                TimeScaleAnimationStart = Time.time;
            }
            //Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, ((TimeScaleAnimationStart + timeScaleAnimationLenght) - Time.time) / timeScaleAnimationLenght);
            Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, timeScaleAnimationLenght * Time.deltaTime / Time.timeScale);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        pcamera.cameraFovScale =  -((speedScale - slowScale) / 2 - Time.timeScale);
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            openMenu(!isMenuOpened);
        }
        if(showNewAvability)
        {

            showNewAvabilityObject.anchoredPosition  = Vector2.Lerp(showNewAvabilityObject.anchoredPosition, showNewAvabilityPlace, ((Time.time - startSceneTime) - showNewAvabilityTime) / showNewAvabilityFadeTime);
        }
    }
    public bool canPlayerUseAbilititiy(Abilitity abilitity)
    {
        if (abilitity.type == AbilitityType.heal && Health == maxHealth)
            return false;

        return true;
    }
    public void UseAbilititiy(GameObject usingObject,Abilitity abilitity)
    {
        bool isPlayer = usingObject == player;
        Enemy enemy;
        bool isEnemy = usingObject.TryGetComponent(out enemy);
        if(usingObject.TryGetComponent(out Rigidbody objectRigidbody))
        {
            objectRigidbody.AddForce(abilitity.forceForward);
            if(isPlayer)
            {
                Vector3 playerPower = playerComponent.movement.playerPowerRaw;
                if (playerPower == Vector3.zero)
                    playerPower = Vector3.left;

                if(abilitity.SpeedLimitTime > 0)
                {
                    PlayerComponent.unlimitSpeed(abilitity.SpeedLimitTime, abilitity.SpeedLimit);
                }
                if(playerPower.x != 0 && playerPower.z != 0)
                {
                    playerPower *= 0.7071f;
                }
                objectRigidbody.AddForce(
                    new Vector3(
                        abilitity.forceDirection * playerPower.x / Time.timeScale,
                        0,
                        abilitity.forceDirection * playerPower.z / Time.timeScale
                    ));
            }
        }
        if(abilitity.type == AbilitityType.projectile || abilitity.type == AbilitityType._default)
        {
            if(isPlayer)
            {
                Vector3 TargetPosiion = Vector3.zero, instantiatePosition = Vector3.zero;
                if (playerComponent.mouseOnMap)
                {
                    TargetPosiion = playerComponent.mouseTargetPoint;
                    TargetPosiion.y = PlayerPosition.y;

                    instantiatePosition = ((TargetPosiion - PlayerPosition).normalized * abilitity.distanceToSpawn) + PlayerPosition;

                    InstantiateProjectile(abilitity.spawn, instantiatePosition, TargetPosiion,abilitity.damage);
                }
                else
                {
                    
                }
                
            }
            else if(isEnemy)
            {
                Vector3 TargetPosion = PlayerPosition, instantiatePosition = Vector3.zero;
                instantiatePosition = ((TargetPosion - enemy.transform.position).normalized * abilitity.distanceToSpawn) + enemy.transform.position;
                InstantiateProjectile(abilitity.spawn, instantiatePosition, TargetPosion, abilitity.damage,false,enemy);
            }
        }
        if(abilitity.type == AbilitityType.heal)
        {
            if(isPlayer)
            {
                if(health < maxHealth)
                    StartCoroutine(playerHeal(abilitity.healpoints, abilitity.healTime, abilitity.healStamp,abilitity.healpoints));
            }
            else if (isEnemy)
            {
                StartCoroutine(EnemyHeal(enemy,abilitity.healpoints, abilitity.healTime, abilitity.healStamp, abilitity.healpoints));
            }
        }
        if(abilitity.type == AbilitityType.throwable)
        {
            if (isPlayer)
            {
                Vector3 TargetPosiion = Vector3.zero, instantiatePosition = Vector3.zero;
                if (playerComponent.mouseOnMap)
                {
                    TargetPosiion = playerComponent.mouseTargetPoint;
                    TargetPosiion.y = PlayerPosition.y;

                    instantiatePosition = ((TargetPosiion - PlayerPosition).normalized * abilitity.distanceToSpawn) + PlayerPosition;

                    Throw(abilitity.spawn, instantiatePosition, TargetPosiion, throwAngle);
                }
                else
                {

                }

            }
            else if (isEnemy)
            {
                Vector3 TargetPosion = PlayerPosition, instantiatePosition = Vector3.zero;
                instantiatePosition = ((TargetPosion - enemy.transform.position).normalized * abilitity.distanceToSpawn) + enemy.transform.position;
                Throw(abilitity.spawn, instantiatePosition, TargetPosion, throwAngle);
            }
        }
        if (isPlayer) updatePlayerAUI();
    }

    //ablities methods
    public void updatePlayerAUI()
    {
        List<PlayerAbility> playerAbilities = PlayerComponent.abilitities;
        List<int> playerAbilitiesCount = PlayerComponent.abilititiesCount;
        int index = 0;
        foreach(PlayerAbility ability in playerAbilities)
        {
            if(ability != null)
            {
                abilititiBars[index].maxCount = ability.count;
                abilititiBars[index].Count = playerAbilitiesCount[index];
                abilititiBars[index].icon.sprite = ability.icon;

                if (playerAbilities[index].Regeneration != null && playerAbilities[index].Regeneration.value != 0)
                {
                    abilititiBars[index].progresAll = ability.Regeneration.count == AbilititiRegenerationCount.every;
                    abilititiBars[index].Progres = PlayerComponent.abilititiesRegenerationState[index] / playerAbilities[index].Regeneration.value;
                }
                else
                {
                    abilititiBars[index].progresAll = false;
                    abilititiBars[index].Progres = 0;
                }
            }
            else
            {
                abilititiBars[index].gameObject.SetActive(false);
            }
            index++;
        }
        float actEnemysCount = FindObjectsOfType<Enemy>().Length;
        enemysCount.text = startEnemysCount - actEnemysCount + " / " + startEnemysCount;
        enemysCountbar.fillAmount = (startEnemysCount - actEnemysCount) / startEnemysCount;

    }
    void InstantiateProjectile(GameObject projectile,Vector3 Postion,Vector3 direction,float damage, bool isPlayer = true,Enemy enemy = null)
    {
        Projectile p2;
        Spike s;
        GameObject projectileObject = Instantiate(projectile, Postion, Quaternion.Euler(0, 0, 0));
        projectileObject.transform.LookAt(direction);
        if(projectileObject.TryGetComponent(out Projectile p))
        {
            p.isEnemy = !isPlayer;
            p.Damage = damage;
        }
        else if((p2 = projectileObject.GetComponentInChildren<Projectile>()) != null)
        {
            p2.isEnemy = !isPlayer;
            p2.Damage = damage;
        }
        else if ((s = projectileObject.GetComponentInChildren<Spike>()) != null)
        {
            s.hp = damage;
            s.isEnemy = !isPlayer;
        }
    }
    void Throw(GameObject ThrowObject,Vector3 from,Vector3 to,float initialAngle)
    {
        GameObject throwObj =  Instantiate(ThrowObject, from, Quaternion.Euler(0, 0, 0));
        if (!throwObj.TryGetComponent(out Rigidbody rigid)) return;
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(to.x, 0, to.z);
        Vector3 planarPostion = new Vector3(from.x, 0, from.z);

        float distance = Vector3.Distance(planarTarget, planarPostion);
        float yOffset = from.y - to.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion) * (to.x > from.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        rigid.velocity = finalVelocity;
    }
    IEnumerator playerHeal(float points,float time,float stamp,float remaindingPoints)
    {
        
        float pointsToadd =points / (time / stamp);
        addHealth(pointsToadd);
        yield return new WaitForSeconds(stamp);
        if(remaindingPoints - pointsToadd > 0)
        {
            StartCoroutine(playerHeal(points, time, stamp, remaindingPoints - pointsToadd));
        }
    }
    IEnumerator EnemyHeal(Enemy enemy, float points, float time, float stamp, float remaindingPoints)
    {

        float pointsToadd = points / (time / stamp);
        enemy.addHealth(pointsToadd);
        yield return new WaitForSeconds(stamp);
        if (remaindingPoints - pointsToadd > 0)
        {
            StartCoroutine(EnemyHeal(enemy,points, time, stamp, remaindingPoints - pointsToadd));
        }
    }
    public static Vector2 balanceSpeed(Vector2 speed, Rigidbody rb)
    {
        return speed / (1 - Time.fixedDeltaTime * rb.drag);
    }
    public static void addHealth(float value)
    {
        get.health = Mathf.Clamp(get.health+value ,0,get.maxHealth);
        //get.updateCanvas();
    }
    public static void takeHealth(float value)
    {
        get.health -= Mathf.Abs(value);
        if(get.health <= 0)
        {
            get.health = 0;
            LevelManager.ReloadScene();
        }
        //get.updateCanvas();
    }
    public void updateCanvas()
    {
        healthText.text = health.ToString();
        healthBarImage.fillAmount = health / maxHealth;
    }
    public static void AddPoints(float value)
    {
        PlayerComponent.AddPoints(value);
        get.updatePlayerAUI();
    }
    public static void AddKills(float value = 1)
    {
        PlayerComponent.AddKills(value);
        get.updatePlayerAUI();
    }
    public static void ClearChldrens(Transform objectTransform)
    {
        for(int i =0; i < objectTransform.childCount;i++)
        {
            Destroy(objectTransform.GetChild(i).gameObject);
        }
    }
    public static bool isPlayer(GameObject obj)
    {
        return (obj.transform.parent != null && obj.transform.parent.gameObject == Player) || obj == Player;
    }
    public void openMenu(bool state)
    {
        isMenuOpened = state;
        menuObject.SetActive(state);
        if(state)
        {
            Time.timeScale = 0.1f;
        }
        else
        {
            Time.timeScale = targetTimeScale;
        }
    }
}
/*
    float gravity = Physics.gravity.magnitude;
    float angle = initialAngle * Mathf.Deg2Rad;
 
    Vector3 planarTarget = new Vector3(p.x, 0, p.z);
    Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);
 
    float distance = Vector3.Distance(planarTarget, planarPostion);
    float yOffset = transform.position.y - p.y;
 
    float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));
 
    Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));
 
    float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion) * (p.x > transform.position.x ? 1 : -1);
    Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;
 
    rigid.velocity = finalVelocity;
 */