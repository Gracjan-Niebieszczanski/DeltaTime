using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerAbilitity", menuName = "Abilitities/PlayerAbility")]
public class PlayerAbility : ScriptableObject
{
    public Abilitity Abilitity;
   
    public int count;
    public AbilititiRegeneration Regeneration;
    

    [Space(30)]
    public string Name;
    public Sprite icon;
    [TextArea(10,30)]
    public string description;
    [Space(30)]
    public bool defaultUnlocked = false;
    public LevelData unlockAfter;
}
