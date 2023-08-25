using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  TMPro;

public class tutorialStepObject : MonoBehaviour
{
    public int priority;
    public TextMeshProUGUI text;
    public RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    void Start()
    {
        
    }

    void Update()
    {
       // rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, text.preferredHeight);
    }
}
