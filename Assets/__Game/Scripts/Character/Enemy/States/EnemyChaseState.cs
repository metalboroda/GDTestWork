namespace GDTestWork
{
  public class EnemyChaseState : State
  {
    public EnemyChaseState(EnemyController enemyController)
    {
      _enemyController = enemyController;
      _enemyMovement = _enemyController.EnemyMovement;
      _characterAnimation = _enemyController.CharacterAnimation;
    }

    private EnemyController _enemyController;
    private EnemyMovement _enemyMovement;
    private CharacterAnimation _characterAnimation;

    public override void Enter()
    {
      _characterAnimation.MovementAnim();
    }

    public override void Update()
    {
      _enemyMovement.Chase(_enemyMovement.MovementSpeed, _enemyMovement.RotationSpeed,
        _enemyMovement.StoppingDistance);
      _characterAnimation.MovementAnimValue(_enemyMovement.GetNormalizedNavMeshAgentVelocity());

      if (_enemyMovement.Chasing() == false)
      {
        _enemyController.StateMachine.ChangeState(new EnemyFightState(_enemyController));
      }
    }
  }
}