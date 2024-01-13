using UnityEngine;

namespace GDTestWork
{
  public abstract class CharacterHandler : MonoBehaviour, IDamageable
  {
    [field: SerializeField] public int MaxHealth = 100;

    [Header("")]
    [SerializeField] protected float invincibilityFrames = 1f;

    public int CurrentHealth { get; set; }

    public virtual void Damage(int damage) { }
  }
}