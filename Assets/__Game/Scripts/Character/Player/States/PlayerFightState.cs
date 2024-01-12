namespace GDTestWork
{
  public class PlayerFightState : State
  {
    public PlayerFightState(PlayerController playerController)
    {
      _playerController = playerController;
    }

    private PlayerController _playerController;
  }
}