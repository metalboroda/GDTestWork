using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GDTestWork
{
  public class PlayerWeaponHandler : CharacterWeaponHandler
  {
    [Header("UI")]
    [SerializeField] private Image mainAttackImg;
    [SerializeField] private Image mainCooldownImg;
    [SerializeField] private Image superAttackImg;
    [SerializeField] private Image superCooldownImg;

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
      CurrentWeapon.Init(enemyLayer, characterController.CharacterAnimation.Animator);

      if (CurrentWeapon != null)
      {
        SetWeaponImages();
      }
      else
      {
        mainAttackImg.gameObject.SetActive(false);
        superAttackImg.gameObject.SetActive(false);
      }
    }

    private void Update()
    {
      if (CurrentWeapon != null)
      {
        DisplayWeaponCooldown();
      }
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

    private void SetWeaponImages()
    {
      mainAttackImg.gameObject.SetActive(true);
      mainAttackImg.sprite = CurrentWeapon.WeaponSO.DamageMainIcon;
      superAttackImg.gameObject.SetActive(true);
      superAttackImg.sprite = CurrentWeapon.WeaponSO.DamageSuperIcon;
    }

    private void DisplayWeaponCooldown()
    {
      UpdateCooldownImage(mainCooldownImg, CurrentWeapon.MainAttackCooldownTimer, CurrentWeapon.WeaponSO.DamageMainCooldown);
      UpdateCooldownImage(superCooldownImg, CurrentWeapon.SuperAttackCooldownTimer, CurrentWeapon.WeaponSO.DamageSuperCooldown);
    }

    private void UpdateCooldownImage(Image cooldownImage, float currentCooldown, float maxCooldown)
    {
      if (maxCooldown > 0)
      {
        float fillAmount = Mathf.Clamp01(currentCooldown / maxCooldown);

        cooldownImage.fillAmount = fillAmount;
      }
      else
      {
        cooldownImage.fillAmount = 1f;
      }
    }

    private void UseWeaponAttackMain()
    {
      if (characterController.StateMachine.CurrentState is not PlayerMovementState) return;

      if (CurrentWeapon.CanAttackMain())
      {
        CurrentWeapon.AttackMain();
        CurrentWeapon.EnableCollider(true);
        characterController.StateMachine.ChangeState(new PlayerAttackState(_playerController));

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
        characterController.StateMachine.ChangeState(new PlayerAttackState(_playerController));

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