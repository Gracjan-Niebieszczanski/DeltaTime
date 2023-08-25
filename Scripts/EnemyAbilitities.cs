using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newEnemyAblities",menuName = "Abilitities/Enemy/Abilitity")]
public class EnemyAbilitities : ScriptableObject
{
    public Abilitity abilitity;
    [Space(10)]
    public int count;
    public AbilititiRegeneration Regeneration;
    [Space(10)]
    public EnemyScheme enemyScheme;
    [Space(10)]
    public float delay;
    public float cooldown;
}
