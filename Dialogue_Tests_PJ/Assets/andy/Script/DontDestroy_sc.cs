using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using UniRx;

public class DontDestroy_sc : MonoBehaviour
{
    public AudioSource audio;

    private void Start()
    {
        Observable.EveryUpdate().Where(_ => Input.GetKeyUp(KeyCode.Space)).Subscribe(_ =>
            audio.Play());
    }

    private void Awake()
    {
        // DontDestroyOnLoad(this);
    }
}