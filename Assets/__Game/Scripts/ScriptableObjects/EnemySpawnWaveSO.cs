using System.Collections.Generic;
using UnityEngine;

namespace GDTestWork
{
  [CreateAssetMenu(menuName = "EnemySpawnWaveSO")]
  public class EnemySpawnWaveSO : ScriptableObject
  {
    [field: SerializeField] public int EnemiesLimit { get; private set; } = 5;
    [field: SerializeField] public List<EnemyController> EnemiesToSpawn = new();
  }
}