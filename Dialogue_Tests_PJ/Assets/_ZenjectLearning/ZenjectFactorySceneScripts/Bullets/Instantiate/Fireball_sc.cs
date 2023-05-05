using _ZenjectLearning.ZenjectFactorySceneScripts.Interface;
using UnityEngine;
using Zenject;

namespace _ZenjectLearning.ZenjectFactorySceneScripts
{
    public class Fireball_sc : BulletBase, IBullet
    {
        [Inject]
        public void Construct(Player_sc player)
        {
            transform.position = player.gameObject.transform.position;
        }

        public class Factory : PlaceholderFactory<Fireball_sc>
        {
        }
    }
}