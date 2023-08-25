using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage;
    public bool isEnemy;
    public Enemy enemy;

    public float speedMultiplayer = 0;
    public float speedTime = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!isEnemy && other.TryGetComponent(out EnemyCollider ec))
        {
            ec.enemy.takeHealth(Damage);
            if (speedTime > 0)
            {
                ec.enemy.AddSlowDown(speedMultiplayer, speedTime);
            }
        }
        else if (isEnemy && Manager.isPlayer(other.gameObject))
        {
            Manager.takeHealth(Damage);
            if(enemy != null)
            {
                enemy.AddPoints(Damage);
                enemy.lastAbilityHited = true;
            }
            if (speedTime > 0)
            {
                Manager.PlayerComponent.AddSlowDown(speedMultiplayer, speedTime);
            }
        }
        Destroy(gameObject);
    }
}