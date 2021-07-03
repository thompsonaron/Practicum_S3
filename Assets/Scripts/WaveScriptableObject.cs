using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/WaveScriptableObject", order = 1)]
public class WaveScriptableObject : ScriptableObject
{
    public int waveTime;
    public EnemyType[] spawnPos1Enemies;
    public EnemyType[] spawnPos2Enemies;
}
