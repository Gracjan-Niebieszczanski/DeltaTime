using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvas : MonoBehaviour
{
    public Image healthBar;
    public float minHealBarSize;
    public float maxHealBarSize;

    RectTransform healthTransflorm;
    void Start()
    {
        healthTransflorm = healthBar.GetComponent<RectTransform>();
        //healthTransflorm.
    }

    void Update()
    {
        
    }
}
