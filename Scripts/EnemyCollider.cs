using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public Enemy enemy;
    private void Awake()
    {
        if(enemy == null)
        {
            Enemy parentEnemy = GetComponentInParent<Enemy>();
            if(parentEnemy != null)
            {
                enemy = parentEnemy;
            }
        }    
    }
}
