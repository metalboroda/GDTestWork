using UnityEngine;

namespace GDTestWork
{
  public class PlayerMovementComp
  {
    public void Movement(float movementSpeed, float rotationSpeed, Vector2 axis,
      CharacterController characterController, Camera camera)
    {
      // Movement
      Vector3 cameraForward = camera.transform.forward;
      Vector3 cameraRight = camera.transform.right;

      cameraForward.y = 0f;
      cameraRight.y = 0f;

      cameraForward.Normalize();
      cameraRight.Normalize();

      Vector3 movement = (cameraForward * axis.y + cameraRight * axis.x) * movementSpeed;

      characterController.SimpleMove(movement);

      // Rotation
      if (movement != Vector3.zero)
      {
        Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);

        characterController.transform.rotation = Quaternion.Slerp(
          characterController.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
      }
    }
  }
}