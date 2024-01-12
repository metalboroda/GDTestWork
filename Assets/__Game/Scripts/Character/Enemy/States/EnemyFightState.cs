using UnityEngine;

namespace GDTestWork
{
  public class EnemyFightState : State
  {
    public EnemyFightState(EnemyController enemyController)
    {
      _enemyController = enemyController;
      _enemyMovement = _enemyController.EnemyMovement;
      _enemyWeaponHandler = _enemyController.EnemyWeaponHandler;
      _characterAnimation = _enemyController.CharacterAnimation;
    }

    private float _timeSinceLastAttack;

    private EnemyController _enemyController;
    private EnemyMovement _enemyMovement;
    private EnemyWeaponHandler _enemyWeaponHandler;
    private CharacterAnimation _characterAnimation;

    public override void Enter()
    {
      _characterAnimation.IdleFightAnim();
    }

    public override void Update()
    {
      _enemyMovement.LookAtTarget(_enemyMovement.RotationSpeed);

      Attack();

      if (_enemyMovement.Chasing() == true)
      {
        _enemyController.StateMachine.ChangeState(new EnemyChaseState(_enemyController));
      }
    }

    public override void Exit()
    {
      _timeSinceLastAttack = 0f;
    }

    private void Attack()
    {
      _timeSinceLastAttack += Time.deltaTime;

      if (_timeSinceLastAttack >= Random.Range(_enemyWeaponHandler.MinAttackRate,
        _enemyWeaponHandler.MaxAttackRate))
      {
        _enemyWeaponHandler.UseWeaponAttackMain();
        _timeSinceLastAttack = 0f;
      }
    }
  }
}