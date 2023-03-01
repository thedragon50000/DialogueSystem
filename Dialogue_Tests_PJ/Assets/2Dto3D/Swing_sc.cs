using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class Swing_sc : MonoBehaviour
{
    public Transform goose;

    public float _positionY;

    // Start is called before the first frame update
    void Start()
    {
        float seconds = Random.Range(0, 5);
print(seconds+"秒");
        // Sequence doYSequence = DOTween.Sequence();
        // doYSequence.Append(
        //     DOTween.To(() => _positionY, i => _positionY = i, 1, 0.6f)).Append(
        //     DOTween.To(() => _positionY, i => _positionY = i, 0, 0.4f)).SetLoops(-1);

        Sequence sequence = DOTween.Sequence();
        // goose.DORotate(new Vector3(0, 0, 10), 2f).SetEase(Ease.InOutSine).Pause();
        // goose.DORotate(new Vector3(0, 70, 0), 2f).SetEase(Ease.InOutBounce).Pause();

        sequence.Append(goose.DORotate(new Vector3(0, 0, 10), 2f).SetEase(Ease.InOutSine))
            .Append(goose.DORotate(new Vector3(0, 70, 0), 2f).SetEase(Ease.InOutBounce))
            .SetLoops(-1, LoopType.Yoyo).Pause();

        Observable.Timer(TimeSpan.FromSeconds(seconds)).Subscribe(_ =>
        {
            sequence.PlayForward();
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        // var goTransform = transform;
        // var position = goTransform.position;
        // position =
        //     new Vector3(position.x, _positionY, position.z);
        // goTransform.position = position;
    }
}