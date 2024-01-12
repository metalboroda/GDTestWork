using UnityEngine;

namespace GDTestWork
{
  public class EnemyController : CharacterControllerBase
  {
    [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; }
    [field: SerializeField] public EnemyWeaponHandler EnemyWeaponHandler { get; private set; }

    private void Start()
    {
      StateMachine.Init(new EnemyChaseState(this));
    }

    private void Update()
    {
      StateMachine.CurrentState.Update();
    }
  }
}