using UnityEngine;

namespace GDTestWork
{
  public class CharacterMovement : MonoBehaviour
  {
    [field: SerializeField] public float MovementSpeed { get; private set; } = 5f;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 10f;
  }
}