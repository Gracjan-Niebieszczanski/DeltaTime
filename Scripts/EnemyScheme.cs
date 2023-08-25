using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyAblities", menuName = "Abilitities/Enemy/Scheme")]
public class EnemyScheme : ScriptableObject
{
    [Header("Distance")]
    public bool distance;
    public float minDistance;
    public float maxDistance;

    [Space(10)]
    [Header("SeePlayer")]
    public bool requireSeePlayer = true;
    public bool requireSeeNotPlayer = false;

    [Space(10)]
    [Header("EnemyHp")]
    public bool requiredHealth;
    public float minHp;
    public float maxHp;

    [Space(10)]
    [Header("PlayerHp")]
    public bool requiredPlayerHealth;
    public float minPlayerHp;
    public float maxPlayerHp;

    [Space(10)]
    [Header("After use")]
    public bool afterUse;
    public Abilitity usedAbility;

    [Space(10)]
    [Header("After hit")]
    public bool afterHit;
    public Abilitity hitedAbility;

}
