using System.Collections;
using UnityEngine;
using Zenject;

namespace GDTestWork
{
  public class PlayerWeaponHandler : CharacterWeaponHandler
  {
    private PlayerController _playerController;

    [Inject] private readonly InputManager _inputManager;

    private void Awake()
    {
      characterController = GetComponent<CharacterControllerBase>();
      _playerController = GetComponent<PlayerController>();
      CurrentWeapon = GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
      CurrentWeapon.Init(enemyLayer, characterController.CharacterAnimation.Animator, this);
    }

    private void OnEnable()
    {
      _inputManager.AttackMainPressed += UseWeaponAttackMain;
      _inputManager.AttackSuperPressed += UseWeaponAttackSuper;
    }

    private void OnDisable()
    {
      _inputManager.AttackMainPressed -= UseWeaponAttackMain;
      _inputManager.AttackSuperPressed -= UseWeaponAttackSuper;
    }

    private void UseWeaponAttackMain()
    {
      if (characterController.StateMachine.CurrentState is not PlayerMovementState) return;

      if (CurrentWeapon.CanAttackMain())
      {
        CurrentWeapon.AttackMain();
        CurrentWeapon.EnableCollider(true);
        characterController.StateMachine.ChangeState(new PlayerAttackingState(_playerController));

        StartCoroutine(DoBackToMovementState());
      }
    }

    private void UseWeaponAttackSuper()
    {
      if (characterController.StateMachine.CurrentState is not PlayerMovementState) return;

      if (CurrentWeapon.CanAttackSuper())
      {
        CurrentWeapon.AttackSuper();
        CurrentWeapon.EnableCollider(true);
        characterController.StateMachine.ChangeState(new PlayerAttackingState(_playerController));

        StartCoroutine(DoBackToMovementState());
      }
    }

    private IEnumerator DoBackToMovementState()
    {
      yield return new WaitForSeconds(characterController.CharacterAnimation.CrossDur);

      float animTime = 0;

      foreach (string anim in CurrentWeapon.AnimationNames)
      {
        if (characterController.CharacterAnimation.Animator.GetCurrentAnimatorStateInfo(0).IsName(anim) == false)
        {
          animTime = characterController.CharacterAnimation.Animator.GetCurrentAnimatorStateInfo(0).length - 0.25f;
        }
      }

      yield return new WaitForSeconds(animTime);

      CurrentWeapon.EnableCollider(false);
      characterController.StateMachine.ChangeState(new PlayerMovementState(_playerController));
    }
  }
}