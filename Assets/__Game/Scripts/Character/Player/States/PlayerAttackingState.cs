namespace GDTestWork
{
  public class PlayerAttackingState : State
  {
    public PlayerAttackingState(PlayerController playerController)
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