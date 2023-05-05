using _ZenjectLearning.ZenjectFactorySceneScripts;
using _ZenjectLearning.ZenjectFactorySceneScripts.Factory;
using _ZenjectLearning.ZenjectFactorySceneScripts.Interface;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    private DiContainer _diContainer;
    public Player_sc player;
    
    public override void InstallBindings()
    {
        _diContainer.Bind<GunType>().AsSingle();
        _diContainer.Bind<GameController_sc>().FromNewComponentOnNewGameObject().AsSingle();
        _diContainer.BindInstance(player);
        
        _diContainer.BindFactoryCustomInterface<IBullet, CustomBulletFactory, IBulletFactory>().FromFactory<CustomBulletFactory>();
        _diContainer.BindFactory<SteelBullet_sc, SteelBullet_sc.Factory>().FromComponentInNewPrefabResource("");
        _diContainer.BindFactory<Fireball_sc, Fireball_sc.Factory>().FromComponentInNewPrefabResource("");
    }
}