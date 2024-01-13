using Lean.Pool;
using System.Collections;
using UnityEngine;

namespace GDTestWork
{
  public class VFXHandler : MonoBehaviour
  {
    [Header("")]
    [SerializeField] private Vector3 offset = Vector3.zero;

    private ParticleSystem _particleSystem;

    private void Awake()
    {
      _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
      StartCoroutine(DoDestroyOnComplete());
    }

    public void SpawnInit()
    {
      transform.position += offset;
    }

    private IEnumerator DoDestroyOnComplete()
    {
      float vfxDuration = _particleSystem.main.duration;

      yield return new WaitForSeconds(vfxDuration);

      LeanPool.Despawn(gameObject);
    }
  }
}