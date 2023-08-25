using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="newLevelData",menuName ="Level/LevelData")]
public class LevelData : ScriptableObject
{
    public string sceneName;
    public bool defaultLevel = false;
    public List<LevelData> unlockOnFinish = new();

}
