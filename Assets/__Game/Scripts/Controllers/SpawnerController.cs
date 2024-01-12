using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTestWork
{
  public class SpawnerController : MonoBehaviour
  {
    [SerializeField] private List<EnemySpawnWaveSO> enemySpawnWaveSOs = new();

    public List<EnemySpawner> EnemySpawners { get; set; } = new();

    private Dictionary<EnemyController, ObjectPool<EnemyController>> _enemyPools;

    private void Start()
    {
      InitializeEnemyPools();
      StartCoroutine(SpawnEnemies());
    }

    public void AddEnemySpawner(EnemySpawner enemySpawner)
    {
      EnemySpawners.Add(enemySpawner);
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
          var enemyPrefab = GetRandomEnemyFromWave(waveSO);

          if (_enemyPools.TryGetValue(enemyPrefab, out var enemyPool))
          {
            spawner.SpawnEnemyAtRandomPoint(enemyPool);
          }
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