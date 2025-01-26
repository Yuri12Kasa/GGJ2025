using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public SpawnerData[] spawnersData;
    private float _timerToNextSpawn = 1f;

    [SerializeField] private Transform _maxSpawnPoint;
    [SerializeField] private Transform _minSpawnPoint;

    private List<Obstacle> _obstaclesToSpawn = new ();
    
    private float _spawnStart = 1.5f;
    private float _spawnEnd = 19.5f;
    private float _minVerticalPoint;
    private float _maxVerticalPoint;
    private float _timer;
    
    private void Start()
    {
        _timer = 0;
        _minVerticalPoint = _minSpawnPoint.position.y;
        _maxVerticalPoint = _maxSpawnPoint.position.y;

        if (GameManagerMauro.Instance)
        {
            foreach (SpawnerData data in spawnersData)
            {
                if (data.playersNumber == GameManagerMauro.Instance.playersNumber)
                {
                    _timerToNextSpawn = data.timerToNextSpawn;
                    _obstaclesToSpawn = data.obstaclesToSpawn.ToList();
                    _spawnStart = data.spawnStart;
                    _spawnEnd = data.spawnEnd;
                }
            }
        }
        
        if (_spawnStart < 0)
        {
            _spawnStart = 0;
        }
        if (_spawnEnd > TimeManager.Instance.SceneTime)
        {
            _spawnEnd = TimeManager.Instance.SceneTime;
        }
    }
    private void Update()
    { 
        if (_spawnStart <= TimeManager.Instance.GetTime())
        {
            if (_spawnEnd >= TimeManager.Instance.GetTime())
            {
                _timer += Time.deltaTime;
                if (_timer < _timerToNextSpawn) 
                    return;
                Spawn();
                _timer = 0;
            }
        }
    }
    private void Spawn()
    {
        var obstacleToSpawn = _obstaclesToSpawn[(int)Randomizer(0, _obstaclesToSpawn.Count)];
        Instantiate(obstacleToSpawn.gameObject, new Vector3(this.transform.position.x,Randomizer(_minVerticalPoint,_maxVerticalPoint)), obstacleToSpawn.transform.rotation);
    }

    private float Randomizer(float min, float max)
    {
        var index = Random.Range(min,max);
        return index;
    }
}
