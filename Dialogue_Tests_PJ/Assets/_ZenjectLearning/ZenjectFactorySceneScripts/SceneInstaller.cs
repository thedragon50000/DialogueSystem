using _ZenjectLearning.ZenjectFactorySceneScripts;
using _ZenjectLearning.ZenjectFactorySceneScripts.Factory;
using _ZenjectLearning.ZenjectFactorySceneScripts.Interface;
using UnityEngine;
using UnityEngine.Rendering;
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
        Container.Bind<GameController_sc>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.BindInstance(player);

        //BindFactoryCustomInterface<
        //(1).要生成的目標interface T,
        //(2).繼承PlaceholderFactory<T>, IValidatable, TFactory的「實例工廠」
        //(3).繼承IFactory<T>的interface TFactory>

        // 在(2)做出差別化，剩下就是各個繼承T的class自己的Factory要注入
        // (Ex. Enemy.cs : T ,  Enemy.cs裡面要有巢狀 class Factory: IFactory<Enemy>
        Container.BindFactoryCustomInterface<
                IBullet,
                CustomBulletFactory,
                IBulletFactory>()
            .FromFactory<CustomBulletFactory>();
        Container.BindFactory<SteelBullet_sc, SteelBullet_sc.Factory>().FromNewComponentOnNewPrefab(bulletSteel);
        Container.BindFactory<Fireball_sc, Fireball_sc.Factory>().FromComponentInNewPrefab(bulletFireball);
        
    }
}