using UnityEngine;

[CreateAssetMenu(fileName = "UnlockedLevels", menuName = "ScriptableObjects/LevelsScriptableObject", order = 1)]
public class UnlockedLevelsScriptableObject : ScriptableObject
{
    public bool level1Locked = false;
    public bool level2Locked = true;
    public bool level3Locked = true;
    public bool level4Locked = true;
    public bool level5Locked = true;
    public bool level6Locked = true;
}
