using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class TestSibling_sc : MonoBehaviour
{
    public Transform transformButton2;


    private void Awake()
    {
        
    }

    void Start()
    {
        var btn = GetComponent<Button>();
        btn.OnClickAsObservable().Subscribe();
        
        var co = Observable.FromCoroutine(CoChangeButtonIndex);
        Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(_ => { co.StartAsCoroutine(); });
    }

    IEnumerator CoChangeButtonIndex()
    {
        yield return null;
        transformButton2.SetSiblingIndex(0);
        yield return new WaitForSeconds(1);
        transformButton2.SetSiblingIndex(5);
        print($"index:{transformButton2.GetSiblingIndex()}");
    }
}