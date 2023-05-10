using _ZenjectLearning.ZenjectFactorySceneScripts;
using _ZenjectLearning.ZenjectFactorySceneScripts.Factory;
using _ZenjectLearning.ZenjectFactorySceneScripts.Interface;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    private DiContainer _diContainer;
    public Player_sc player;
    public SteelBullet_sc bulletSteel;
    public Fireball_sc bulletFireball;

    public override void InstallBindings()
    {
        Container.Bind<GunType>().AsSingle();
        Container.Bind<GameController_sc>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInstance(player);

        Container.BindFactoryCustomInterface<IBullet, CustomBulletFactory, IBulletFactory>()
            .FromFactory<CustomBulletFactory>();
        Container.BindFactory<SteelBullet_sc, SteelBullet_sc.Factory>().FromNewComponentOnNewPrefab(bulletSteel);
        Container.BindFactory<Fireball_sc, Fireball_sc.Factory>().FromNewComponentOnNewPrefab(bulletFireball);
    }
}