using System;
using UnityEngine;
using UniRx;
using DG.Tweening;

namespace _ZenjectLearning.ZenjectFactorySceneScripts.Interface
{
    public class BulletBase : MonoBehaviour
    {
        public Transform transform;

        public void Awake()
        {
            transform = GetComponent<Transform>();
        }

        public void Start()
        {
            float positionY = transform.position.y;
            transform.DOLocalMoveY(positionY + 10, 3).OnComplete(() => { });
        }
    }
}