using UnityEngine;
using Zenject;

public class PuzzleInstaller : MonoInstaller
{
    public PuzzleManager_sc puzzleManger;

    public override void InstallBindings()
    {
        Container.BindInstance(puzzleManger).AsSingle().NonLazy();
    }
}