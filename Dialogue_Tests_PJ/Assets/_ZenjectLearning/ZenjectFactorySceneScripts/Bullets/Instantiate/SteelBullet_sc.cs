using _ZenjectLearning.ZenjectFactorySceneScripts.Interface;
using UnityEngine;
using Zenject;

namespace _ZenjectLearning.ZenjectFactorySceneScripts
{
    public class SteelBullet_sc : BulletBase, IBullet
    {
        [Inject]
        public void Construct(Player_sc player)
        {
            transform.position = player.gameObject.transform.position;
        }
        public class Factory : PlaceholderFactory<SteelBullet_sc>
        {
            
        }
    }
}