using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(Manager.isPlayer(other.gameObject))
        {
            if(FindObjectsOfType<Enemy>().Length == 0)
            {
                LevelManager.SetThisLevelCompleted();
                LevelManager.LoadNextLevel();
            }
            
        }
    }
}
