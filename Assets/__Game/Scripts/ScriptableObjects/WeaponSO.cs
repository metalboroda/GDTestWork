using UnityEngine;
using UnityEngine.UI;

namespace GDTestWork
{
  [CreateAssetMenu(menuName = "WeaponSO")]
  public class WeaponSO : ScriptableObject
  {
    [field: SerializeField] public int DamageMain { get; private set; }
    [field: SerializeField] public float DamageMainCooldown { get; private set; } = 1f;
    [field: SerializeField] public Image DamageMainIcon { get; private set; }
    [field: SerializeField] public int DamageSuper { get; private set; }
    [field: SerializeField] public float DamageSuperCooldown { get; private set; } = 2f;
    [field: SerializeField] public Image DamageSuperIcon { get; private set; }
  }
}