using Lean.Pool;
using System.Collections;
using UnityEngine;

namespace GDTestWork
{
  public class VFXHandler : MonoBehaviour
  {
    [Header("")]
    [SerializeField] private float spawnTime = 1f;
    [SerializeField] private Vector3 offset = Vector3.zero;

    private ParticleSystem _particleSystem;

    private void Awake()
    {
      _particleSystem = GetComponent<ParticleSystem>();
    }

    public void SpawnInit(Transform transform)
    {
      _particleSystem.Play();
      transform.position += offset;

      LeanPool.Despawn(gameObject, spawnTime);
    }
  }
}