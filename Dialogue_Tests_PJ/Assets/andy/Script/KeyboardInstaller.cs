using Doublsb.Dialog;
using UnityEngine;
using Zenject;

public class KeyboardInstaller : MonoInstaller
{
    public AudioManager audio;

    public override void InstallBindings()
    {
        Container.Bind<KeyboradControl>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Without_MonoBehavier>().AsSingle();

        Container.BindInstance(audio).AsSingle();
        Container.Bind<GameRunner>().AsSingle();
        // Container.Bind<IInitializable>().To<GameRunner>().AsSingle();
    }

    // public class GameRunner : IInitializable
    public class GameRunner
    {
        private readonly AudioManager audio;

        public GameRunner(AudioManager _audio)
        {
            audio = _audio;
        }
    }
}