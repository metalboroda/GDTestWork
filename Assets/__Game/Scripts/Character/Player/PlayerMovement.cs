using UnityEngine;
using Zenject;

namespace GDTestWork
{
  public class PlayerMovement : CharacterMovement
  {
    public CharacterController CharacterController { get; private set; }

    private Camera _cameraMain;

    private PlayerMovementComp _playerMovementComp = new();

    [Inject] private InputManager _inputManager;

    private void Awake()
    {
      _cameraMain = Camera.main;
      CharacterController = GetComponent<CharacterController>();
    }

    public void Movement(float movementSpeed, float rotationSpeed)
    {
      _playerMovementComp.Movement(movementSpeed, rotationSpeed,
        _inputManager.GetMovementAxis(), CharacterController, _cameraMain);
    }

    public float GetNormalizedVelocityMagnitude()
    {
      Vector3 velocity2D = new(CharacterController.velocity.x, 0f, CharacterController.velocity.z);
      float normalizedMagnitude = velocity2D.magnitude / MovementSpeed;

      return Mathf.Clamp01(normalizedMagnitude);
    }
  }
}