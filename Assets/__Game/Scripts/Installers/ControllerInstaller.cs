using UnityEngine;
using Zenject;

namespace GDTestWork
{
  public class ControllerInstaller : MonoInstaller
  {
    [SerializeField] private SpawnerController spawnerController;

    public override void InstallBindings()
    {
      Container.Bind<SpawnerController>().FromInstance(spawnerController).AsSingle();
    }
  }
}