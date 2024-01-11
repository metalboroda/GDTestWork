using UnityEngine;

namespace GDTestWork
{
  [CreateAssetMenu(menuName = "WeaponAnimSO")]
  public class WeaponAnimSO : ScriptableObject
  {
    [field: SerializeField] public string AttackMain { get; private set; }
    [field: SerializeField] public string AttackSuper { get; private set; }
  }
}