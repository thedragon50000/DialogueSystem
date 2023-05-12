using _ZenjectLearning.ZenjectFactorySceneScripts.Interface;
using UnityEngine;
using Zenject;

namespace _ZenjectLearning.ZenjectFactorySceneScripts.Factory
{
    public class CustomBulletFactory : PlaceholderFactory<IBullet>, IValidatable, IBulletFactory
    {
        private SteelBullet_sc.Factory _steelFactory;
        private Fireball_sc.Factory _fireFactory;
        private GunType _gunType;

        public CustomBulletFactory(SteelBullet_sc.Factory s, Fireball_sc.Factory f, GunType g)
        {
            _steelFactory = s;
            _fireFactory = f;
            _gunType = g;
        }

        public override IBullet Create()
        {
            IBullet b = _gunType.EGunType == EGunType.FireMagic ? _fireFactory.Create() : _steelFactory.Create();

            return b;
        }

        public void Validate()
        {
            Debug.Log("Validate Factory");
            _fireFactory.Create();
            _steelFactory.Create();
        }
    }
}