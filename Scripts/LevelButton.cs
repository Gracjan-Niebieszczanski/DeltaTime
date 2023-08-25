using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{

    public int level;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void SelectLevel()
    {
        MenuManager.get.SelectLevel(level);
    }    
}
