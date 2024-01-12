namespace GDTestWork
{
  public class EnemyFightState : State
  {
    public EnemyFightState(EnemyController enemyController)
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
      _characterAnimation.IdleFightAnim();
    }

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