using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GDTestWork
{
  public class SpawnerController : MonoBehaviour
  {
    public static SpawnerController Instance;

    [SerializeField] private List<EnemySpawnWaveSO> enemySpawnWaveSOs = new();

    public List<EnemySpawner> EnemySpawners { get; set; } = new();

    private readonly List<EnemyController> _spawnedEnemies = new();
    private Dictionary<EnemyController, ObjectPool<EnemyController>> _enemyPools;
    private Dictionary<EnemySpawnWaveSO, int> _spawnedEnemiesCount;
    private int _removedEnemiesCount;

    private void Awake()
    {
      Instance = this;
    }

    private void Start()
    {
      _spawnedEnemiesCount = new Dictionary<EnemySpawnWaveSO, int>();

      InitializeEnemyPools();
      StartCoroutine(SpawnEnemies());
      StartCoroutine(CheckAndStartNewWave());
    }

    public void AddEnemySpawner(EnemySpawner enemySpawner)
    {
      EnemySpawners.Add(enemySpawner);
    }

    public void AddSpawnedEnemy(EnemyController enemy)
    {
      _spawnedEnemies.Add(enemy);
    }

    public void RemoveSpawnedEnemy(EnemyController enemy)
    {
      _spawnedEnemies.Remove(enemy);
      _removedEnemiesCount++;
    }

    private void InitializeEnemyPools()
    {
      _enemyPools = new Dictionary<EnemyController, ObjectPool<EnemyController>>();

      foreach (var waveSO in enemySpawnWaveSOs)
      {
        foreach (var enemyPrefab in waveSO.EnemiesToSpawn)
        {
          if (_enemyPools.ContainsKey(enemyPrefab) == false)
          {
            ObjectPool<EnemyController> pool = new(enemyPrefab, 10);

            _enemyPools.Add(enemyPrefab, pool);
          }
        }
      }
    }

    private IEnumerator SpawnEnemies()
    {
      while (true)
      {
        yield return new WaitForSeconds(1f);

        foreach (var spawner in EnemySpawners)
        {
          var waveSO = GetRandomWave();

          if (!_spawnedEnemiesCount.ContainsKey(waveSO))
          {
            _spawnedEnemiesCount.Add(waveSO, 0);
          }

          if (_spawnedEnemiesCount[waveSO] < waveSO.EnemiesLimit)
          {
            var enemyPrefab = GetRandomEnemyFromWave(waveSO);

            if (_enemyPools.TryGetValue(enemyPrefab, out var enemyPool))
            {
              spawner.SpawnEnemyAtRandomPoint(enemyPool);

              _spawnedEnemiesCount[waveSO]++;
            }
          }
        }
      }
    }

    private IEnumerator CheckAndStartNewWave()
    {
      while (true)
      {
        yield return null;

        foreach (var waveSO in _spawnedEnemiesCount.Keys.ToList())
        {
          if (_removedEnemiesCount >= waveSO.EnemiesLimit)
          {
            yield return new WaitForSeconds(3f);

            _removedEnemiesCount = 0;
            _spawnedEnemiesCount.Remove(waveSO);

            var index = enemySpawnWaveSOs.IndexOf(waveSO);
            var nextIndex = (index + 1) % enemySpawnWaveSOs.Count;
            var nextWaveSO = enemySpawnWaveSOs[nextIndex];

            _spawnedEnemiesCount.Add(nextWaveSO, 0);

            //Debug.Log($"Starting new wave: {nextWaveSO.name}");
          }
        }

        if (_spawnedEnemiesCount.Count == 0)
        {
          _removedEnemiesCount = 0;
          _spawnedEnemiesCount.Add(enemySpawnWaveSOs[0], 0);

          //Debug.Log("Restarting from the first wave");
        }
      }
    }

    private EnemySpawnWaveSO GetRandomWave()
    {
      return enemySpawnWaveSOs[Random.Range(0, enemySpawnWaveSOs.Count)];
    }

    private EnemyController GetRandomEnemyFromWave(EnemySpawnWaveSO waveSO)
    {
      return waveSO.EnemiesToSpawn[Random.Range(0, waveSO.EnemiesToSpawn.Count)];
    }
  }
}