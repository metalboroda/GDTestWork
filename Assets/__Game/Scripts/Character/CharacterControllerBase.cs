using UnityEngine;

namespace GDTestWork
{
  public class CharacterControllerBase : MonoBehaviour
  {
    [field: SerializeField] public CharacterAnimation CharacterAnimation { get; private set; }

    public StateMachine StateMachine { get; private set; } = new();
  }
}