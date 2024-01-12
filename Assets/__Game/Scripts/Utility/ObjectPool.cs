using System.Collections.Generic;
using UnityEngine;

namespace GDTestWork
{
  public class ObjectPool<T> where T : MonoBehaviour
  {
    private readonly T _prefab;
    private readonly int _poolSize = 100;
    private List<T> _objectPool;

    public ObjectPool(T prefab, int poolSize)
    {
      _prefab = prefab;
      _poolSize = poolSize;

      InitializePool();
    }

    private void InitializePool()
    {
      _objectPool = new List<T>();

      for (int i = 0; i < _poolSize; i++)
      {
        T obj = Object.Instantiate(_prefab);

        obj.gameObject.SetActive(false);
        _objectPool.Add(obj);
      }
    }

    public T GetObjectFromPool(Vector3 position, Quaternion rotation, Transform parent)
    {
      foreach (T obj in _objectPool)
      {
        if (obj.gameObject.activeInHierarchy == false)
        {
          obj.transform.SetPositionAndRotation(position, rotation);
          obj.gameObject.SetActive(true);

          return obj;
        }
      }

      T newObj = Object.Instantiate(_prefab, position, rotation, parent);

      _objectPool.Add(newObj);

      return newObj;
    }

    public void ReturnObjectToPool(T obj)
    {
      obj.gameObject.SetActive(false);
    }
  }
}