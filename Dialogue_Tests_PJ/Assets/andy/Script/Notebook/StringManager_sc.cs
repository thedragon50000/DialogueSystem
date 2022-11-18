using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEditor;
using Zenject;

public class StringManager_sc : MonoBehaviour
{
    // private string strAnswer = "";
    public StringReactiveProperty strAnswer;


    public void Start()
    {
        strAnswer.Subscribe(CheckAnswer, Error, Complete);
    }

    [Inject]
    void Init()
    {
        print("StringManager_sc initialized");
    }

    private void Complete()
    {
        print("OnCompleted");
    }

    private void Error(Exception e)
    {
        Debug.LogError(e);
    }

    private void CheckAnswer(string obj)
    {
        string check = obj;
        string answer0 = E_WordPuzzleObj.A.ToString() + E_WordPuzzleObj.B + E_WordPuzzleObj.C; //ABC

        if (check.Contains(answer0))
        {
            print("ABC triggered");
        }
    }
}