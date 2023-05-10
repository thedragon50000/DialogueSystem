using _ZenjectLearning.ZenjectFactorySceneScripts.Factory;
using _ZenjectLearning.ZenjectFactorySceneScripts.Interface;
using UnityEngine;
using Zenject;

namespace _ZenjectLearning.ZenjectFactorySceneScripts
{
    public class GameController_sc : MonoBehaviour
    {
        [Inject] private GunType _gunType;
        private CustomBulletFactory _customBulletFactory;

        IBullet CreateBullet()
        {
            EGunType type = _gunType.EGunType;
            _gunType.EGunType = type == EGunType.Riffle ? EGunType.FireMagic : EGunType.Riffle;

            IBullet b = _customBulletFactory.Create();
            return b;
        }
    }
}