using UnityEngine;

namespace GDTestWork
{
  [CreateAssetMenu(menuName = "WeaponSO")]
  public class WeaponSO : ScriptableObject
  {
    [field: SerializeField] public int Damage { get; private set; }
  }
}