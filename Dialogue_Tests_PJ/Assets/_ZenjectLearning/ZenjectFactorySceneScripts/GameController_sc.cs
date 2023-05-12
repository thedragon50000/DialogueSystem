using System;
using _ZenjectLearning.ZenjectFactorySceneScripts.Factory;
using _ZenjectLearning.ZenjectFactorySceneScripts.Interface;
using UniRx;
using UnityEngine;
using Zenject;

namespace _ZenjectLearning.ZenjectFactorySceneScripts
{
    public class GameController_sc : MonoBehaviour
    {
        [Inject] private GunType _gunType;
        [Inject] private Player_sc _player;
        [Inject] private IBulletFactory _bulletFactory;
        
        private void Start()
        {
            Observable.EveryUpdate().Subscribe(_ =>
            {
                float axis = Input.GetAxis("Horizontal") * .1f;
                // print("Updating." + axis);
                _player.position.Value += new Vector3(axis, 0);
            });
        }

        private void Update()
        {
            var bullet = CreateBullet();
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // print("Press Space.");
                // var bullet = CreateBullet();
            }
        }

        IBullet CreateBullet()
        {
            EGunType type = _gunType.EGunType;
            _gunType.EGunType = type == EGunType.Riffle ? EGunType.FireMagic : EGunType.Riffle;

            IBullet b = _bulletFactory.Create();
            return b;
        }
    }
}