using System;
using _ZenjectLearning.ZenjectFactorySceneScripts.Factory;
using UnityEngine;
using UniRx;
using DG.Tweening;
using Zenject;

namespace _ZenjectLearning.ZenjectFactorySceneScripts.Interface
{
    public class BulletBase : MonoBehaviour
    {
        public void Start()
        {
            float positionY = transform.position.y;
            transform.DOLocalMoveY(positionY + 9, 3).OnComplete(() => { Destroy(gameObject); });
        }
    }
}