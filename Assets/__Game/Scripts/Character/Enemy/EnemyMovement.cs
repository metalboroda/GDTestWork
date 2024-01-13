using UnityEngine;

namespace GDTestWork
{
  public class EnemyMovement : CharacterMovement
  {
    [field: SerializeField] public float StoppingDistance { get; private set; } = 2.5f;

    public Transform TargetToMove { get; private set; }

    private readonly EnemyChaseComp _enemyChaseComp = new();

    private EnemyController _enemyController;

    private void Awake()
    {
      _enemyController = GetComponent<EnemyController>();
    }

    private void Start()
    {
      TargetToMove = PlayerController.Instance.transform;
    }

    public void Chase(float movementSpeed, float rotationSpeed, float stoppingDistance)
    {
      _enemyChaseComp.Chase(movementSpeed, rotationSpeed, stoppingDistance,
        TargetToMove, _enemyController.NavMeshAgent);
    }

    public bool Chasing()
    {
      if (TargetToMove == null)
      {
        return true;
      }

      return _enemyChaseComp.Chasing(TargetToMove, _enemyController.NavMeshAgent);
    }

    public void LookAtTarget(float rotationSpeed)
    {
      _enemyChaseComp.LookAtTarget(rotationSpeed, TargetToMove, transform);
    }

    public float GetNormalizedNavMeshAgentVelocity()
    {
      if (_enemyController.NavMeshAgent != null)
      {
        float velocityMagnitude = _enemyController.NavMeshAgent.velocity.magnitude;
        float maxSpeed = _enemyController.NavMeshAgent.speed;
        float normalizedVelocity = velocityMagnitude / maxSpeed;

        normalizedVelocity = Mathf.Clamp01(normalizedVelocity);

        return normalizedVelocity;
      }

      return 0f;
    }
  }
}