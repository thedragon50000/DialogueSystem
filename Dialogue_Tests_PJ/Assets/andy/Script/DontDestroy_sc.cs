using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class DontDestroy_sc : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}