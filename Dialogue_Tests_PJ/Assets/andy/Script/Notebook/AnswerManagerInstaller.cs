using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AnswerManagerInstaller : MonoInstaller
{
    public StringManager_sc _stringManagerSc;

    public override void InstallBindings()
    {
        // Container.Bind<StringManager_sc>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.BindInstance(_stringManagerSc).AsSingle();
    }

}