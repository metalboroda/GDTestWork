using System.Collections;
using UnityEngine;

namespace GDTestWork
{
  public class EnemyWeaponHandler : CharacterWeaponHandler
  {
    [field: SerializeField] public float MinAttackRate { get; private set; } = 1f;
    [field: SerializeField] public float MaxAttackRate { get; private set; } = 2f;

    private void Awake()
    {
      characterController = GetComponent<CharacterControllerBase>();
      CurrentWeapon = GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
      CurrentWeapon.Init(enemyLayer, characterController.CharacterAnimation.Animator);
    }

    public override void UseWeaponAttackMain()
    {
      CurrentWeapon.AttackMain();
      CurrentWeapon.EnableCollider(true);

      StartCoroutine(DoDisableColliderAfterAttack());
    }

    private IEnumerator DoDisableColliderAfterAttack()
    {
      yield return new WaitForSeconds(characterController.CharacterAnimation.CrossDur);

      float animLength = 0;

      foreach (string anim in CurrentWeapon.AnimationNames)
      {
        if (characterController.CharacterAnimation.Animator.GetCurrentAnimatorStateInfo(0).IsName(anim) == true)
        {
          animLength = characterController.CharacterAnimation.Animator.GetCurrentAnimatorStateInfo(0).length;
        }
      }

      StartCoroutine(CurrentWeapon.DoEnableCollider(false, animLength - 0.6f));

      yield return new WaitForSeconds(animLength - 0.25f);

      CurrentWeapon.EnableCollider(false);
    }
  }
}