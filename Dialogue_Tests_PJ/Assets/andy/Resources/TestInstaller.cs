using Doublsb.Dialog;
using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
    public AudioManager audioPrefab;
    public DialogManager dialogPrefab;
    
    
    
    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromComponentInNewPrefab(audioPrefab).AsSingle().NonLazy();
        
        Container.Bind<DialogManager>().FromComponentInNewPrefab(dialogPrefab).AsSingle().NonLazy();
        // dialog = Container.InstantiatePrefabForComponent<DialogManager>(dialogPrefab);
        // Container.BindInstance(audio).AsSingle();
        // Container.Bind<DialogManager>().FromInstance(dialog).AsSingle();
        
    }
}