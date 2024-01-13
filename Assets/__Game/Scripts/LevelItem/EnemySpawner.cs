using UnityEngine;
using Zenject;

namespace GDTestWork
{
  public class EnemySpawner : MonoBehaviour
  {
    [SerializeField] private float distanceToPlayer = 7.5f;

    private BoxCollider _boxCollider;

    private PlayerController _playerController;

    [Inject] private SpawnerController _spawnerController;

    private void Awake()
    {
      _boxCollider = GetComponent<BoxCollider>();

      _spawnerController.AddEnemySpawner(this);
    }

    private void Start()
    {
      _playerController = PlayerController.Instance;
    }

    public void SpawnEnemyAtRandomPoint(ObjectPool<EnemyController> enemyPool)
    {
      var randPos = GetRandomPointInsideCollider();
      var randRot = Quaternion.Euler(0, Random.Range(-360, 360), 0);
      var spawnedEnemy = enemyPool.GetObjectFromPool(randPos, randRot, null);

      spawnedEnemy.SpawnInit(enemyPool);
      _spawnerController.AddSpawnedEnemy(spawnedEnemy);
    }

    public void SpawnEnemyAtRandomPoint(EnemyController enemy)
    {
      var randPos = GetRandomPointInsideCollider();
      var randRot = Quaternion.Euler(0, Random.Range(-360, 360), 0);

      Instantiate(enemy, randPos, randRot, null);
    }

    private Vector3 GetRandomPointInsideCollider()
    {
      Vector3 randomPoint;

      do
      {
        randomPoint = new Vector3(
            Random.Range(_boxCollider.bounds.min.x, _boxCollider.bounds.max.x),
            0f,
            Random.Range(_boxCollider.bounds.min.z, _boxCollider.bounds.max.z)
        );
      } while (Vector3.Distance(randomPoint,
      _playerController.transform.position) < distanceToPlayer);

      return randomPoint;
    }
  }
}