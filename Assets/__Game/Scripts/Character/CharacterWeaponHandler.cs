using UnityEngine;

namespace GDTestWork
{
  public class CharacterWeaponHandler : MonoBehaviour
  {
    [SerializeField] protected LayerMask enemyLayer;

    [field: Header("Anim param's")]
    [field: SerializeField] public float CrossDur { get; private set; } = 0.2f;

    public Weapon CurrentWeapon { get; set; }

    protected CharacterControllerBase characterController;
  }
}