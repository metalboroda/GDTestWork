using System.Collections.Generic;
using UnityEngine;

namespace GDTestWork
{
  public class Weapon : MonoBehaviour
  {
    [SerializeField] private WeaponSO weaponSO;

    [Header("")]
    [SerializeField] private WeaponAnimSO weaponAnimSO;

    public List<string> AnimationNames { get; private set; } = new();

    public LayerMask _enemyLayer;

    //private List<GameObject> _attackedObjects = new();

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

    private void OnTriggerEnter(Collider other)
    {
      //if (_attackedObjects.Contains(other.gameObject) == true) return;

      if ((_enemyLayer.value & 1 << other.gameObject.layer) != 0)
      {
        //_attackedObjects.Add(other.gameObject);

        if (other.TryGetComponent(out IDamageable damageable))
        {
          damageable.Damage(weaponSO.Damage);
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

      /*if (enable == false)
      {
        _attackedObjects.Clear();
      }*/
    }

    public void AttackMain()
    {
      _characterAnimator.CrossFadeInFixedTime(weaponAnimSO.AttackMain, 0.1f);
    }

    public void AttackSuper()
    {
      _characterAnimator.CrossFadeInFixedTime(weaponAnimSO.AttackSuper, 0.1f);
    }

    private void InitAnimNames()
    {
      AnimationNames.Add(weaponAnimSO.AttackMain);
      AnimationNames.Add(weaponAnimSO.AttackSuper);
    }
  }
}