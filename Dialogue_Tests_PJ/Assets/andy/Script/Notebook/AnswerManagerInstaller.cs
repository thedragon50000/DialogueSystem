using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AnswerManagerInstaller : MonoInstaller
{
    public StringManager_sc stringManagerSc;
    public ButtonList_sc buttonListSc;

    public override void InstallBindings()
    {
        // Container.Bind<StringManager_sc>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.BindInstance(stringManagerSc).AsSingle();
        Container.BindInstance(buttonListSc).AsSingle();
        
    }

}