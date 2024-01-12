using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GDTestWork
{
  public class PlayerWeaponHandler : CharacterWeaponHandler
  {
    [Header("Enemy Detection")]
    [SerializeField] private float detectionRadius = 5f;

    [Header("UI")]
    [SerializeField] private Image mainAttackImg;
    [SerializeField] private Image mainAttackMouse;
    [SerializeField] private Image mainCooldownImg;
    [SerializeField] private Image superAttackImg;
    [SerializeField] private Image superAttackMouse;
    [SerializeField] private Image superCooldownImg;

    private bool _canSuperAttack;

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
        DetectEnemies();
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

    override public void UseWeaponAttackMain()
    {
      if (characterController.StateMachine.CurrentState is not PlayerMovementState) return;

      if (CurrentWeapon.CanAttackMain())
      {
        CurrentWeapon.AttackMain();
        CurrentWeapon.EnableCollider(true);
        characterController.StateMachine.ChangeState(new PlayerFightState(_playerController));

        StartCoroutine(DoBackToMovementState());
      }
    }

    override public void UseWeaponAttackSuper()
    {
      if (characterController.StateMachine.CurrentState is not PlayerMovementState) return;
      if (_canSuperAttack == false) return;

      if (CurrentWeapon.CanAttackSuper())
      {
        CurrentWeapon.AttackSuper();
        CurrentWeapon.EnableCollider(true);
        characterController.StateMachine.ChangeState(new PlayerFightState(_playerController));

        StartCoroutine(DoBackToMovementState());
      }
    }

    private IEnumerator DoBackToMovementState()
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

      characterController.StateMachine.ChangeState(new PlayerMovementState(_playerController));
    }

    private void EnableSuperAttack(bool enable)
    {
      superAttackMouse.gameObject.SetActive(enable);
      _canSuperAttack = enable;
    }

    private void DetectEnemies()
    {
      bool collisionDetected = Physics.CheckSphere(transform.position, detectionRadius, enemyLayer);

      if (collisionDetected)
      {
        EnableSuperAttack(true);
      }
      else
      {
        EnableSuperAttack(false);
      }
    }
  }
}