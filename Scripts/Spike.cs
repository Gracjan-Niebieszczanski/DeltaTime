using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    
    public float hp;
    public float delay;
    public bool limitTime = true;
    public float time = 0.2f;

    public bool isEnemy;

    public float speedMultiplayer = 0;
    public float speedTime = 0;
    private void Awake()
    {
        Invoke("OnCollider", delay);
    }
    void Start()
    {
        if (limitTime) Invoke("End", time + delay);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isEnemy && other.TryGetComponent(out EnemyCollider enemy))
        {
            enemy.enemy.takeHealth(hp);
            if (speedTime > 0)
            {
                enemy.enemy.AddSlowDown(speedMultiplayer, speedTime);
            }
        }
        if (isEnemy && Manager.isPlayer(other.gameObject))
        {
            Manager.takeHealth(hp);
            if(speedTime > 0)
            {
                Manager.PlayerComponent.AddSlowDown(speedMultiplayer, speedTime);
            }
        }
    }

    void End()
    {
        Destroy(this);
    }
    void OnCollider()
    {
        GetComponent<Collider>().enabled = true;
    }
}
