using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;

public class Punch_sc : MonoBehaviour
{
    void Start()
    {
        Observable.EveryUpdate().Where(_ => Input.GetKeyUp(KeyCode.Space)).Subscribe(_ =>
            {
                print("AAA");
                transform.DOScale(Vector3.one * 15, 1);
            }
        );
    }
}