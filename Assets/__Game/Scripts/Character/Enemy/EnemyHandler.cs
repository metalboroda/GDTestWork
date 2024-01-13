using UnityEngine;

namespace GDTestWork
{
  public class EnemyHandler : CharacterHandler
  {
    [Header("")]
    [SerializeField] private int healthReward = 10;

    private EnemyController _enemyController;

    private void Awake()
    {
      _enemyController = GetComponent<EnemyController>();
    }

    private void Start()
    {
      CurrentHealth = MaxHealth;
    }

    public override void Damage(int damage)
    {
      CurrentHealth -= damage;

      if (CurrentHealth <= 0)
      {
        CurrentHealth = 0;

        if (_enemyController.InPool == true)
        {
          _enemyController.EnemyPool.ReturnObjectToPool(_enemyController);
        }
        else
        {
          Destroy(gameObject);
        }

        _enemyController.SpawnerController.RemoveSpawnedEnemy(_enemyController);

        EventManager.RaisePlayerHealthIncreased(healthReward);
      }
    }
  }
}