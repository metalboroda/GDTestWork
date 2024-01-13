namespace GDTestWork
{
  public class EnemyDeathState : State
  {
    public EnemyDeathState(EnemyController enemyController)
    {
      _enemyController = enemyController;
      _characterAnimation = _enemyController.CharacterAnimation;
    }

    private EnemyController _enemyController;
    private CharacterAnimation _characterAnimation;

    public override void Enter()
    {
      _characterAnimation.DeathAnim();
    }
  }
}