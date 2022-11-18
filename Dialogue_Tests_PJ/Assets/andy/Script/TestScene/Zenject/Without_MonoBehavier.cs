using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Zenject;

// public class Without_MonoBehavier : IInitializable, ITickable
public class Without_MonoBehavier : ITickable
{
    public int i = 222;

    public void Show()
    {
        Debug.Log($"call Show({i})");
    }

    // [Inject]
    // public void Initialize()
    // {
    //     MonoBehaviour.print("Without_MonoBehavier Initialize in Start()");
    //     i = 333;
    // }

    public void Tick()
    {
        MonoBehaviour.print("Ticking");
    }
}