using UnityEngine;
using Zenject;

namespace GDTestWork
{
  public class ManagerInstaller : MonoInstaller
  {
    [SerializeField] private InputManager inputManager;

    public override void InstallBindings()
    {
      Container.Bind<InputManager>().FromInstance(inputManager).AsSingle();
    }
  }
}