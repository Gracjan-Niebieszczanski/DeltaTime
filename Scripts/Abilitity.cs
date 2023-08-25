using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AbilitityType
{
    other,
    hit,
    projectile,
    throwable,
    heal,
    rush,
    hook,
    _default
    
}
[CreateAssetMenu(fileName = "newAbilitity",menuName = "Abilitities/Abilitity")]
public class Abilitity : ScriptableObject
{
   
    public AbilitityType type;

    [Header("ActionOnUse")]
    [Header("Force")]
    public Vector3 forceForward;
    public float forceDirection;
    public float duration;
    public float SpeedLimitTime = 0;
    public float SpeedLimit = 3;
    [Space(20)]
    [Header("projectile")]
    public GameObject spawn;
    public bool rotationForward = true;
    public float distanceToSpawn = 0.3f;
    [Space(20)]
    public float damage;
    public float maxDistance;
    [Header("health")]
    public float healpoints;
    public float healTime;
    public float healStamp = 1f;

}
