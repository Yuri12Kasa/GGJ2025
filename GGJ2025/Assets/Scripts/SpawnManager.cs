using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [FormerlySerializedAs("objsToSpawn")] public List<Obstacle> obstaclesToSpawn = new List<Obstacle>();
    public float spawnStart = 1.5f;
    public float spawnEnd = 19.5f;
    public float timerToNextSpawn = 1f;

    private float timer;
    [SerializeField] private Transform _maxSpawnPoint;
    [SerializeField] private Transform _minSpawnPoint;

    [SerializeField] private float minVerticalPoint;
    [SerializeField] private float maxVerticalPoint;
    private void Start()
    {
        timer = 0;
        minVerticalPoint = _minSpawnPoint.position.y;
        maxVerticalPoint = _maxSpawnPoint.position.y;
        
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
        if (spawnStart <= TimeManager.Instance.GetTime())
        {
            if (spawnEnd >= TimeManager.Instance.GetTime())
            {
                timer += Time.deltaTime;
                if (timer < timerToNextSpawn) return;
                Spawn();
                timer = 0;
            }
        }
    }
    private void Spawn()
    {
        var obstacleToSpawn = obstaclesToSpawn[(int)Randomaizer(0, obstaclesToSpawn.Count)];
        Instantiate(obstacleToSpawn.gameObject, new Vector3(this.transform.position.x,Randomaizer(minVerticalPoint,maxVerticalPoint)), obstacleToSpawn.transform.rotation);
    }

    private float Randomaizer(float min, float max)
    {
        var index = Random.Range(min,max);
        return index;
    }
}
