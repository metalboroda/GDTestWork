namespace GDTestWork
{
  public class PlayerMovementState : State
  {
    public PlayerMovementState(PlayerController playerController)
    {
      _playerController = playerController;
      _playerMovement = _playerController.PlayerMovement;
      _characterAnimation = _playerController.CharacterAnimation;
    }

    private PlayerController _playerController;
    private PlayerMovement _playerMovement;
    private CharacterAnimation _characterAnimation;

    public override void Enter()
    {
      _characterAnimation.MovementAnim();
    }

    public override void Update()
    {
      _playerMovement.Movement(_playerMovement.MovementSpeed, _playerMovement.RotationSpeed);
      _characterAnimation.MovementAnimValue(_playerMovement.GetNormalizedVelocityMagnitude());
    }
  }
}