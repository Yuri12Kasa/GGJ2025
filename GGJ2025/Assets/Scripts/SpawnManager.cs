using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [FormerlySerializedAs("objsToSpawn")] public List<Obstacle> obstaclesToSpawn = new List<Obstacle>();
    public float spawnStart;
    public float spawnEnd;
   
    private void Start()
    {
        if (spawnStart < 0)
        {
            spawnStart = 0;
        }
        if (spawnEnd > GameManagerMauro.Instance.mainSceneTime)
        {
            spawnEnd = GameManagerMauro.Instance.mainSceneTime;
        }
    }
    private void Update()
    {
        if (spawnStart > TimeManager.Instance.SceneTime && spawnEnd<= TimeManager.Instance.SceneTime )//TimeHendler
        {
            Spawn();
        }
    }
    private void Spawn()
    {
        var index = Random.Range(0, obstaclesToSpawn.Count);
        var obstacleToSpawn = obstaclesToSpawn[index];
        Instantiate(obstacleToSpawn, obstacleToSpawn.transform.position, obstacleToSpawn.transform.rotation);
    }
}
