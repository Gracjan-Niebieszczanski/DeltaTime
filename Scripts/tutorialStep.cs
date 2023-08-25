using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum windowType
{
    none,
    Factory,
    Magazine,
    Sell,
    Buy,
    Recipte
}
public enum changeType
{
    window,
    button,
    time
}
[CreateAssetMenu(fileName = "new Tutorial", menuName = "TutorialStep")]
public class tutorialStep : ScriptableObject
{
    //public changeType changeType;
    //public windowType windowType;
    [TextArea(30,50)]
    public string text;
    // public GameObject toLockButtons;
    //public float timeToChange;
    //public Vector2 position;
}
