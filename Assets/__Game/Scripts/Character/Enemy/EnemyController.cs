using UnityEngine;
using UnityEngine.AI;

namespace GDTestWork
{
  public class EnemyController : CharacterControllerBase
  {
    [field: Header("")]
    [field: SerializeField] public EnemyType EnemyType { get; private set; }

    [field: Header("")]
    [field: SerializeField] public EnemyHandler EnemyHandler { get; private set; }
    [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; }
    [field: SerializeField] public EnemyWeaponHandler EnemyWeaponHandler { get; private set; }

    public NavMeshAgent NavMeshAgent { get; private set; }
    public CapsuleCollider CapsuleCollider { get; private set; }

    public SpawnerController SpawnerController { get; private set; }

    private void Awake()
    {
      NavMeshAgent = GetComponent<NavMeshAgent>();
      CapsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
      SpawnerController = SpawnerController.Instance;
      StateMachine.Init(new EnemyChaseState(this));
    }

    private void Update()
    {
      StateMachine.CurrentState.Update();
    }

    public void SpawnInit()
    {
      StateMachine.Init(new EnemyChaseState(this));
      CapsuleCollider.enabled = true;
      NavMeshAgent.enabled = true;
      EnemyHandler.CurrentHealth = EnemyHandler.MaxHealth;
    }
  }
}