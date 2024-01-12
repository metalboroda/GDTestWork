namespace GDTestWork
{
  public class EnemyChaseState : State
  {
    public EnemyChaseState(EnemyController enemyController)
    {
      _enemyController = enemyController;
      _enemyMovement = _enemyController.EnemyMovement;
    }

    private EnemyController _enemyController;
    private EnemyMovement _enemyMovement;

    public override void Update()
    {
      _enemyMovement.Chase(_enemyMovement.MovementSpeed, _enemyMovement.RotationSpeed,
        _enemyMovement.StoppingDistance);

      if (_enemyMovement.Chasing() == false)
      {
        _enemyController.StateMachine.ChangeState(new EnemyAttackState(_enemyController));
      }
    }
  }
}