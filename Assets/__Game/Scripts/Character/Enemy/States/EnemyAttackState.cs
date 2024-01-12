namespace GDTestWork
{
  public class EnemyAttackState : State
  {
    public EnemyAttackState(EnemyController enemyController)
    {
      _enemyController = enemyController;
      _enemyMovement = _enemyController.EnemyMovement;
    }

    private EnemyController _enemyController;
    private EnemyMovement _enemyMovement;

    public override void Update()
    {
      _enemyMovement.LookAtTarget(_enemyMovement.RotationSpeed);

      if (_enemyMovement.Chasing() == true)
      {
        _enemyController.StateMachine.ChangeState(new EnemyChaseState(_enemyController));
      }
    }
  }
}