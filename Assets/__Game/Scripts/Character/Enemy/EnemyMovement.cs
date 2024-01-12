using UnityEngine;
using UnityEngine.AI;

namespace GDTestWork
{
  public class EnemyMovement : CharacterMovement
  {
    [field: SerializeField] public float StoppingDistance { get; private set; } = 2.5f;

    public Transform TargetToMove { get; private set; }

    private readonly EnemyChaseComp _enemyChaseComp = new();

    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
      _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
      TargetToMove = PlayerController.Instance.transform;
    }

    public void Chase(float movementSpeed, float rotationSpeed, float stoppingDistance)
    {
      _enemyChaseComp.Chase(movementSpeed, rotationSpeed, stoppingDistance,
        TargetToMove, _navMeshAgent);
    }

    public bool Chasing()
    {
      return _enemyChaseComp.Chasing(TargetToMove, _navMeshAgent);
    }

    public void LookAtTarget(float rotationSpeed)
    {
      _enemyChaseComp.LookAtTarget(rotationSpeed, TargetToMove, transform);
    }

    public float GetNormalizedNavMeshAgentVelocity()
    {
      // Ensure _navMeshAgent is not null
      if (_navMeshAgent != null)
      {
        float velocityMagnitude = _navMeshAgent.velocity.magnitude;
        float maxSpeed = _navMeshAgent.speed;
        float normalizedVelocity = velocityMagnitude / maxSpeed;

        normalizedVelocity = Mathf.Clamp01(normalizedVelocity);

        return normalizedVelocity;
      }

      return 0f;
    }
  }
}