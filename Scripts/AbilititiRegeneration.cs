using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilititiRegenerationType
{
    none,
    time,
    points,
    kills
}
public enum AbilititiRegenerationCount
{ 
    one,
    every
}
[CreateAssetMenu(fileName = "newAbilititiRegeneration", menuName = "Abilitities/AbilititiRegeneration")]
public class AbilititiRegeneration : ScriptableObject
{
    public AbilititiRegenerationType type;
    public AbilititiRegenerationCount count;
    public float value;
}
