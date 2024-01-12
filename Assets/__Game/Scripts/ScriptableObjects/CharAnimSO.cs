using UnityEngine;

namespace GDTestWork
{
  [CreateAssetMenu(menuName = "CharAnimSO")]
  public class CharAnimSO : ScriptableObject
  {
    [field: SerializeField] public string MovementBlendAnim { get; private set; }
    [field: SerializeField] public string IdleFightAnim { get; private set; }

    [field: Header("Values")]
    [field: SerializeField] public string MovementBlendValue { get; private set; }
  }
}