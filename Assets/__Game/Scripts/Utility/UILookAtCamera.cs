using UnityEngine;

namespace GDTestWork
{
  public class UILookAtCamera : MonoBehaviour
  {
    private Camera mainCamera;

    private void Awake()
    {
      mainCamera = Camera.main;
    }

    private void Update()
    {
      transform.LookAt(mainCamera.transform);
    }
  }
}