using UnityEngine;
using Zenject;

namespace GDTestWork
{
  public class ManagerInstaller : MonoInstaller
  {
    [SerializeField] private InputManager inputManager;
    [SerializeField] private UIManager uiManager;

    public override void InstallBindings()
    {
      Container.Bind<InputManager>().FromInstance(inputManager).AsSingle();
      Container.Bind<UIManager>().FromInstance(uiManager).AsSingle();
    }
  }
}