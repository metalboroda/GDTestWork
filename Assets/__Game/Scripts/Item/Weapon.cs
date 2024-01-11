using System.Collections.Generic;
using UnityEngine;

namespace GDTestWork
{
  public class Weapon : MonoBehaviour
  {
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private WeaponAnimSO weaponAnimSO;

    public List<string> AnimationNames { get; private set; } = new();

    private LayerMask _enemyLayer;
    private int _currentDamage;
    private float _mainAttackCooldownTimer;
    private float _superAttackCooldownTimer;

    private CapsuleCollider _capsuleCollider;
    private Animator _characterAnimator;
    private CharacterWeaponHandler _characterWeaponHandler;

    private void Awake()
    {
      _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
      _capsuleCollider.enabled = false;
      InitAnimNames();
    }

    private void Update()
    {
      _mainAttackCooldownTimer -= Time.deltaTime;
      _superAttackCooldownTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
      if ((_enemyLayer.value & 1 << other.gameObject.layer) != 0)
      {
        if (other.TryGetComponent(out IDamageable damageable))
        {
          damageable.Damage(_currentDamage);
        }
      }
    }

    public void Init(LayerMask enemyLayer, Animator animator, CharacterWeaponHandler characterWeaponHandler)
    {
      _enemyLayer = enemyLayer;
      _characterAnimator = animator;
      _characterWeaponHandler = characterWeaponHandler;
    }

    public void EnableCollider(bool enable)
    {
      _capsuleCollider.enabled = enable;
    }

    public void AttackMain()
    {
      if (CanAttackMain())
      {
        _currentDamage = weaponSO.DamageMain;
        _characterAnimator.CrossFadeInFixedTime(weaponAnimSO.AttackMain, 0.1f);
        _mainAttackCooldownTimer = weaponSO.DamageMainCooldown;
      }
    }

    public void AttackSuper()
    {
      if (CanAttackSuper())
      {
        _currentDamage = weaponSO.DamageSuper;
        _characterAnimator.CrossFadeInFixedTime(weaponAnimSO.AttackSuper, 0.1f);
        _superAttackCooldownTimer = weaponSO.DamageSuperCooldown;
      }
    }

    public bool CanAttackMain()
    {
      return _mainAttackCooldownTimer <= 0f;
    }

    public bool CanAttackSuper()
    {
      return _superAttackCooldownTimer <= 0f;
    }

    private void InitAnimNames()
    {
      AnimationNames.Add(weaponAnimSO.AttackMain);
      AnimationNames.Add(weaponAnimSO.AttackSuper);
    }
  }
}