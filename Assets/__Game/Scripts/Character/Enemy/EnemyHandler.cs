using Lean.Pool;
using System.Collections;
using UnityEngine;

namespace GDTestWork
{
  public class EnemyHandler : CharacterHandler
  {
    [Header("")]
    [SerializeField] private int healthReward = 10;

    [Header("Trixter")]
    [SerializeField] private int doppelAmount = 2;
    [SerializeField] private EnemyController doppelPrefab;

    [Header("VFX")]
    [SerializeField] private VFXHandler spawnVFX;
    [SerializeField] private VFXHandler destroyVFX;

    private CapsuleCollider _capsuleCollider;

    private EnemyController _enemyController;

    private void Awake()
    {
      _capsuleCollider = GetComponent<CapsuleCollider>();

      _enemyController = GetComponent<EnemyController>();
    }

    private void Start()
    {
      _capsuleCollider.enabled = false;

      CurrentHealth = MaxHealth;

      StartCoroutine(InvincibilityFrames());
      SpawnVFX();
    }

    public override void Damage(int damage)
    {
      CurrentHealth -= damage;

      if (CurrentHealth <= 0)
      {
        CurrentHealth = 0;

        Death();
      }
    }

    private void Death()
    {
      if (_enemyController.EnemyType == EnemyType.Trixter)
      {
        SpawnDoppel();
      }

      _enemyController.SpawnerController.RemoveSpawnedEnemy(_enemyController);

      EventManager.RaisePlayerHealthIncreased(healthReward);

      DestroyVFX();

      if (_enemyController.InPool == true)
      {
        _enemyController.EnemyPool.ReturnObjectToPool(_enemyController);
      }
      else
      {
        Destroy(gameObject);
      }
    }

    private void SpawnDoppel()
    {
      float randPosMult = Random.Range(1.5f, 2.5f);
      Vector3 spawnPos = new(transform.position.x + randPosMult,
        0, transform.position.z + randPosMult);

      for (int i = 0; i < doppelAmount; i++)
      {
        Instantiate(doppelPrefab, spawnPos, transform.rotation);
      }
    }

    private IEnumerator InvincibilityFrames()
    {
      yield return new WaitForSeconds(invincibilityFrames);

      _capsuleCollider.enabled = true;
    }

    private void SpawnVFX()
    {
      var spawnedVFX = LeanPool.Spawn(spawnVFX, transform.position, Quaternion.identity);

      spawnedVFX.SpawnInit();
    }

    private void DestroyVFX()
    {
      var spawnedVFX = LeanPool.Spawn(destroyVFX, transform.position, Quaternion.identity);

      spawnedVFX.SpawnInit();
    }
  }
}