using DG.Tweening;
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

    private EnemyController _enemyController;

    private void Awake()
    {
      _enemyController = GetComponent<EnemyController>();
    }

    private void Start()
    {
      _enemyController.CapsuleCollider.enabled = false;

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
      _enemyController.StateMachine.ChangeState(new EnemyDeathState(_enemyController));

      if (_enemyController.EnemyType == EnemyType.Trixter)
      {
        SpawnDoppel();
      }

      _enemyController.CapsuleCollider.enabled = false;
      _enemyController.NavMeshAgent.enabled = false;
      _enemyController.SpawnerController.RemoveSpawnedEnemy(_enemyController);

      EventManager.RaisePlayerHealthIncreased(healthReward);

      StartCoroutine(DoDissapear());
    }

    public IEnumerator DoDissapear()
    {
      yield return new WaitForSeconds(2);

      transform.DOMoveY(-2, 1).SetSpeedBased(true).OnComplete(() =>
      {
        LeanPool.Despawn(gameObject);
      });
    }

    private void SpawnDoppel()
    {
      float randPosMult = Random.Range(1.5f, 2.5f);
      Vector3 spawnPos = new(transform.position.x + randPosMult,
        0, transform.position.z + randPosMult);

      for (int i = 0; i < doppelAmount; i++)
      {
        var spawnedDoppel = LeanPool.Spawn(doppelPrefab, spawnPos, transform.rotation);

        spawnedDoppel.SpawnInit();
      }
    }

    private IEnumerator InvincibilityFrames()
    {
      yield return new WaitForSeconds(invincibilityFrames);

      _enemyController.CapsuleCollider.enabled = true;
    }

    private void SpawnVFX()
    {
      var spawnedVFX = LeanPool.Spawn(spawnVFX, transform.position, Quaternion.identity);

      spawnedVFX.SpawnInit();
    }
  }
}