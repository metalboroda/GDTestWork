using UnityEngine;

namespace GDTestWork
{
  public class PlayerController : CharacterControllerBase
  {
    public static PlayerController Instance;

    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField] public PlayerWeaponHandler PlayerWeaponHandler { get; private set; }

    private void Awake()
    {
      Instance = this;
      StateMachine.Init(new PlayerMovementState(this));
    }

    private void Update()
    {
      StateMachine.CurrentState.Update();
    }
  }
}