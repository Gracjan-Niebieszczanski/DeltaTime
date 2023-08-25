using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHealthModifier : MonoBehaviour
{
    public bool active = true;
    [Header("Times")]
    public float firstTime;
    public float SecondTime;
    [Header("Hp")]
    public float hp;
    public bool increasing;
    public float increasingPower;
    [Header("Clamp")]
    public bool clamp;
    public float min;
    public float max;


    public List<GameObject> objectInside = new();
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject != null && objectInside.IndexOf(collision.gameObject) == -1)
        {
            objectInside.Add(collision.gameObject);
            StartCoroutine(Modifie(collision.gameObject, firstTime));
        }
        
    }
    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject != null && objectInside.IndexOf(collision.gameObject) == -1)
        {
            objectInside.Add(collision.gameObject);
            StartCoroutine(Modifie(collision.gameObject, firstTime));

        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject != null)
        {
            objectInside.Remove(collision.gameObject);
        }
        
    }
    IEnumerator Modifie(GameObject obj, float times)
    {
        yield return new WaitForSeconds(SecondTime);
        if (obj != null && objectInside.IndexOf(obj) != -1)
        {

            int type = 0;
            bool isPlayer = Manager.isPlayer(obj);
            bool isEnemy = obj.TryGetComponent(out EnemyCollider ec);

            if (isPlayer || isEnemy)
            {
                if (active)
                {
                    if(isPlayer)
                    {
                        type = 1;
                    }
                    else
                    {
                        type = 2;
                    }
                    modifie(obj, times,type);
                }
                
                StartCoroutine(Modifie(obj, times + 1));
                
            }
            
        }
        
    }
    void modifie(GameObject obj,float times,int type)
    {
        

        float objHealth = 0;
        bool isPlayer = type == 1;
        bool isEnemy = type == 2;
        Enemy ec = null;
        if (isPlayer)
        {
            objHealth = Manager.Health;
        }
        else
        {
            ec = obj.GetComponentInParent<Enemy>();
            objHealth = ec.hp;
        }
        times++;
        float clampHp;
        float actHp = hp;
        if(increasing)
        {
            actHp = hp +  hp * (times * increasingPower);
        }
        if (clamp)
        {
            clampHp = Mathf.Clamp(objHealth + actHp, min, max) - objHealth;
        }
        else
        {
            clampHp = actHp;
        }

        if(clampHp >= 0)
        {
            if(isPlayer)
            {
                Manager.addHealth(Mathf.Abs(clampHp));
            }
            else
            {
                ec.addHealth(Mathf.Abs(clampHp));
            }
        }
        else
        {
            if (isPlayer)
            {
                
                Manager.takeHealth(Mathf.Abs(clampHp));
            }
            else
            {
                ec.takeHealth(Mathf.Abs(clampHp));
            }

        }
        
    }
    public void SetActive(bool newActive)
    {
        active = newActive;
    }
}
