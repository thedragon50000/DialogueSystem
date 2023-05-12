using System;
using UnityEngine;
using UniRx;

namespace _ZenjectLearning.ZenjectFactorySceneScripts
{
    public class Player_sc : MonoBehaviour
    {
        public Vector3ReactiveProperty position;

        private void Start()
        {
            //當玩家移動
            position.Subscribe(Move);
        }

        private void Move(Vector3 obj)
        {
            position.Value = obj;
            gameObject.transform.position = position.Value;
        }
        
    }
}