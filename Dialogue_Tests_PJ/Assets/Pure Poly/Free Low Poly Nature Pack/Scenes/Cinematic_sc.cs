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
        Tweener tweener;
        Vector3 forward = transform.position + Vector3.back * 40;
        _sequence = transform.DOJump(forward, 1, 10, 10);
        _sequence.Pause();
        _sequence.SetAutoKill(true);
        // _sequence.Append(tweener).Insert()
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