using UnityEngine;
using UnityEngine.AI;

namespace GDTestWork
{
  public class EnemyChaseComp
  {
    public void Chase(float movementSpeed, float rotationSpeed, float stoppingDistance,
        Transform target, NavMeshAgent navMeshAgent)
    {
      navMeshAgent.speed = movementSpeed;
      navMeshAgent.angularSpeed = rotationSpeed;
      navMeshAgent.stoppingDistance = stoppingDistance;

      if (target != null)
      {
        navMeshAgent.destination = target.position;
      }
    }

    public bool Chasing(Transform target, NavMeshAgent navMeshAgent)
    {
      bool chasing = true;

      if (Vector3.Distance(navMeshAgent.transform.position, target.position) <= navMeshAgent.stoppingDistance)
      {
        chasing = false;
      }
      else
      {
        chasing = true;
      }

      return chasing;
    }

    public void LookAtTarget(float rotationSpeed, Transform target, Transform self)
    {
      Vector3 direction = (target.position - self.position).normalized;
      Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

      self.rotation = Quaternion.Slerp(self.rotation, lookRotation, Time.deltaTime * rotationSpeed / 50);
    }
  }
}