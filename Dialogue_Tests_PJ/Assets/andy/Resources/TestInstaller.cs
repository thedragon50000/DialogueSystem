using Doublsb.Dialog;
using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
    public AudioManager audioPrefab;
    public DialogManager dialogPrefab;
    public AudioManager audio;
    public DialogManager dialog;
    
    
    
    public override void InstallBindings()
    {
        audio = Container.InstantiatePrefabForComponent<AudioManager>(audioPrefab);
        Container.Bind<AudioManager>().FromInstance(audio).AsSingle().NonLazy();
        
        Container.Bind<DialogManager>().FromComponentInNewPrefab(dialogPrefab).AsSingle().NonLazy();
        // dialog = Container.InstantiatePrefabForComponent<DialogManager>(dialogPrefab);
        // Container.BindInstance(audio).AsSingle();
        // Container.Bind<DialogManager>().FromInstance(dialog).AsSingle();
    }
}