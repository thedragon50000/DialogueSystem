using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using Unity.VisualScripting;
using Sequence = DG.Tweening.Sequence;

public class Cinematic_sc : MonoBehaviour
{
    private Sequence _sequence;

    private void Awake()
    {
        Vector3 forward = transform.position + Vector3.back * 40;

        _sequence = DOTween.Sequence();

        var doJump = transform.DOJump(forward, 1, 10, 10).Pause();
        _sequence.SetAutoKill(true);
        var doRotate = transform.DORotate(transform.rotation.eulerAngles+Vector3.back, 5);
        _sequence.Append(doJump).Join(doRotate);
    }

    void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _sequence.PlayForward();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
    }
}