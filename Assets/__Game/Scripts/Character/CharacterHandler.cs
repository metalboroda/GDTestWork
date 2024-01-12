using UnityEngine;

namespace GDTestWork
{
  public abstract class CharacterHandler : MonoBehaviour, IDamageable
  {
    [field: SerializeField] public int Health = 100;

    public int CurrentHealth { get; set; }

    public virtual void Damage(int damage) { }
  }
}