using System.Collections;
using UnityEngine;

namespace GDTestWork
{
  public class EnemyWeaponHandler : CharacterWeaponHandler
  {
    [field: SerializeField] public float MinAttackRate { get; private set; } = 1.5f;
    [field: SerializeField] public float MaxAttackRate { get; private set; } = 2.5f;

    private EnemyController _enemyController;

    private void Awake()
    {
      characterController = GetComponent<CharacterControllerBase>();
      _enemyController = GetComponent<EnemyController>();
      CurrentWeapon = GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
      CurrentWeapon.Init(enemyLayer, characterController.CharacterAnimation.Animator);
    }

    public override void UseWeaponAttackMain()
    {
      CurrentWeapon.AttackMain();
    }
  }
}