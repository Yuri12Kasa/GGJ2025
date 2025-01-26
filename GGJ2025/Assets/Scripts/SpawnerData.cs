using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerData", menuName = "Scriptable Objects/SpawnerData")]
public class SpawnerData : ScriptableObject
{
    [Range(1,6)]
    public int playerNumber = 1;
    public float timerToNextSpawn = 5;
    public Obstacle[] obstaclesToSpawn;
    public float spawnStart = 3;
    public float spawnEnd = 17;
}
