namespace GDTestWork
{
  public class PlayerAttackState : State
  {
    public PlayerAttackState(PlayerController playerController)
    {
      _playerController = playerController;
      _characterAnimation = _playerController.CharacterAnimation;
    }

    private PlayerController _playerController;
    private CharacterAnimation _characterAnimation;

    public override void Update()
    {
    }
  }
}