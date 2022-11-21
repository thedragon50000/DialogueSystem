using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEditor;
using Zenject;

public class StringManager_sc : MonoBehaviour
{
    // private string strAnswer = "";
    public StringReactiveProperty strAnswer;
    public TMP_Text instantText;
    [Inject] private StringManager_sc stringManager;

    


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
        instantText.text = check;
        string answer0 = E_WordPuzzleObj.我.ToString() + E_WordPuzzleObj.喜歡 + E_WordPuzzleObj.USER; //ABC

        if (check.Equals(answer0))
        {
            print("對答案 triggered");
        }
    }
}