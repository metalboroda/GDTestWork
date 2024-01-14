using UnityEngine;
using Zenject;

namespace GDTestWork
{
  public class ControllerInstaller : MonoInstaller
  {
    [SerializeField] private SceneController sceneController;
    [SerializeField] private SpawnerController spawnerController;

    public override void InstallBindings()
    {
      Container.Bind<SceneController>().FromInstance(sceneController).AsSingle();
      Container.Bind<SpawnerController>().FromInstance(spawnerController).AsSingle();
    }
  }
}