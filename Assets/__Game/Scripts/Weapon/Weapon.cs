using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTestWork
{
  public class Weapon : MonoBehaviour
  {
    [field: SerializeField] public WeaponSO WeaponSO;
    [SerializeField] private WeaponAnimSO weaponAnimSO;

    public List<string> AnimationNames { get; private set; } = new();
    public float MainAttackCooldownTimer { get; private set; }
    public float SuperAttackCooldownTimer { get; private set; }

    private LayerMask _enemyLayer;
    private int _currentDamage;

    private CapsuleCollider _capsuleCollider;
    private Animator _characterAnimator;

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
      MainAttackCooldownTimer -= Time.deltaTime;
      SuperAttackCooldownTimer -= Time.deltaTime;

      if (MainAttackCooldownTimer <= 0)
      {
        MainAttackCooldownTimer = 0;
      }

      if (SuperAttackCooldownTimer <= 0)
      {
        SuperAttackCooldownTimer = 0;
      }
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

    public void Init(LayerMask enemyLayer, Animator animator)
    {
      _enemyLayer = enemyLayer;
      _characterAnimator = animator;
    }

    public void EnableCollider(bool enable)
    {
      _capsuleCollider.enabled = enable;
    }

    public IEnumerator DoEnableCollider(bool enable, float delay)
    {
      yield return new WaitForSeconds(delay);

      _capsuleCollider.enabled = enable;
    }

    public void AttackMain()
    {
      if (CanAttackMain())
      {
        _currentDamage = WeaponSO.DamageMain;
        _characterAnimator.CrossFadeInFixedTime(weaponAnimSO.AttackMain, 0.1f);
        MainAttackCooldownTimer = WeaponSO.DamageMainCooldown;
      }
    }

    public void AttackSuper()
    {
      if (CanAttackSuper())
      {
        _currentDamage = WeaponSO.DamageSuper;
        _characterAnimator.CrossFadeInFixedTime(weaponAnimSO.AttackSuper, 0.1f);
        SuperAttackCooldownTimer = WeaponSO.DamageSuperCooldown;
      }
    }

    public bool CanAttackMain()
    {
      return MainAttackCooldownTimer <= 0f;
    }

    public bool CanAttackSuper()
    {
      return SuperAttackCooldownTimer <= 0f;
    }

    private void InitAnimNames()
    {
      AnimationNames.Add(weaponAnimSO.AttackMain);
      AnimationNames.Add(weaponAnimSO.AttackSuper);
    }
  }
}