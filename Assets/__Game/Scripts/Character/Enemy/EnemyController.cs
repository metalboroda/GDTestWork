using UnityEngine;

namespace GDTestWork
{
  public class EnemyController : CharacterControllerBase
  {
    [field: SerializeField] public EnemyHandler EnemyHandler { get; private set; }
    [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; }
    [field: SerializeField] public EnemyWeaponHandler EnemyWeaponHandler { get; private set; }

    public ObjectPool<EnemyController> EnemyPool { get; private set; }

    private void Start()
    {
      StateMachine.Init(new EnemyChaseState(this));
    }

    private void Update()
    {
      StateMachine.CurrentState.Update();
    }

    public void SpawnInit(ObjectPool<EnemyController> enemyPool)
    {
      EnemyPool = enemyPool;
      StateMachine.Init(new EnemyChaseState(this));
      EnemyHandler.CurrentHealth = EnemyHandler.Health;
    }
  }
}