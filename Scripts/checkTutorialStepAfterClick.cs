using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkTutorialStepAfterClick : MonoBehaviour
{
    public List<int> tutorialIndexes;
    private void Awake()
    {
        if(TryGetComponent(out Button thisButton))
        {
            thisButton.onClick.AddListener(clicked);
        }
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void clicked()
    {
        if(tutorialIndexes.IndexOf(TutorialManager.get.tutorialIndex) != -1)
        {
            TutorialManager.get.changeTutorialStep();
        }
    }
}
