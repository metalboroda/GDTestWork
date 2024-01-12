using UnityEngine;

namespace GDTestWork
{
  public class EnemyController : CharacterControllerBase
  {
    [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; }

    private void Awake()
    {
      StateMachine.Init(new EnemyChaseState(this));
    }

    private void Update()
    {
      StateMachine.CurrentState.Update();
    }
  }
}