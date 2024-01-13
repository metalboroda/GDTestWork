using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTestWork
{
  public class SpawnerController : MonoBehaviour
  {
    public static SpawnerController Instance;

    [SerializeField] private float delayBetweenWaves = 3f;
    [SerializeField] private List<EnemySpawnWaveSO> enemyWaves = new();

    private List<EnemySpawner> _enemySpawners = new();
    private List<EnemyController> _spawnedEnemies = new();
    private int _removedEnemiesCount;
    private int _currentWaveIndex = 0;
    private int _overallWaveIndex = 0;

    private void Awake()
    {
      Instance = this;
    }

    private void Start()
    {
      StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
      while (_currentWaveIndex < enemyWaves.Count)
      {
        yield return new WaitForSeconds(delayBetweenWaves);

        SpawnWave(enemyWaves[_currentWaveIndex]);

        yield return new WaitUntil(() => _removedEnemiesCount >= enemyWaves[_currentWaveIndex].EnemiesLimit);

        _currentWaveIndex++;

        if (_currentWaveIndex >= enemyWaves.Count)
        {
          _currentWaveIndex = 0;

          ShuffleWaves();
        }

        _overallWaveIndex++;

        EventManager.RaiseEnemyWaveChanged(_overallWaveIndex);
      }
    }

    private void SpawnWave(EnemySpawnWaveSO wave)
    {
      _removedEnemiesCount = 0;

      foreach (var spawner in _enemySpawners)
      {
        StartCoroutine(SpawnEnemiesFromSpawner(spawner, wave));
      }
    }

    private IEnumerator SpawnEnemiesFromSpawner(EnemySpawner spawner, EnemySpawnWaveSO wave)
    {
      for (int i = 0; i < wave.EnemiesLimit; i++)
      {
        var enemyToSpawn = wave.EnemiesToSpawn[Random.Range(0, wave.EnemiesToSpawn.Count)];

        spawner.SpawnEnemyAtRandomPoint(enemyToSpawn);

        yield return new WaitForSeconds(wave.DelayBetweenEnemies);
      }
    }

    public void AddEnemySpawner(EnemySpawner enemySpawner)
    {
      _enemySpawners.Add(enemySpawner);
    }

    public void AddSpawnedEnemy(EnemyController enemy)
    {
      _spawnedEnemies.Add(enemy);
    }

    public void RemoveSpawnedEnemy(EnemyController enemy)
    {
      _spawnedEnemies.Remove(enemy);
      _removedEnemiesCount++;

      /*if (_removedEnemiesCount >= enemyWaves[_currentWaveIndex].EnemiesLimit)
      {
      }*/
    }

    private void ShuffleWaves()
    {
      for (int i = 0; i < enemyWaves.Count; i++)
      {
        EnemySpawnWaveSO temp = enemyWaves[i];
        int randomIndex = Random.Range(i, enemyWaves.Count);

        enemyWaves[i] = enemyWaves[randomIndex];
        enemyWaves[randomIndex] = temp;
      }
    }
  }
}