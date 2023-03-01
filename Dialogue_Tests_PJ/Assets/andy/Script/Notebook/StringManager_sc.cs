using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEditor;
using Zenject;

public class StringManager_sc : MonoBehaviour
{
    // private string strAnswer = "";
    public StringReactiveProperty strAnswer;
    public TMP_Text instantText;

    [Inject] private DialogManager _dialogManager;
    [Inject] private ButtonList_sc _buttonListSc;
    public GameObject answerItem;


    void DialogShow(List<DialogData> dialogData)
    {
        _dialogManager.Show(dialogData);
    }

    public void Start()
    {
        strAnswer.Subscribe(CheckAnswer, Error, Complete);
        answerItem.transform.GetOrAddComponent<Rigidbody>();
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
        var printer = instantText.transform.parent.gameObject;

        string answer0 = E_WordPuzzleObj.我.ToString() + E_WordPuzzleObj.喜歡 + E_WordPuzzleObj.USER; //ABC

        if (check.Equals(answer0))
        {
            print("對答案 triggered");
            int i = (int) E_WordPuzzleObj.我;
            RightAnswerCloseButton(i);
            i = (int) E_WordPuzzleObj.喜歡;
            RightAnswerCloseButton(i);
            i = (int) E_WordPuzzleObj.USER;
            RightAnswerCloseButton(i);

            printer.SetActive(false);

            SpecialDiaologue_ItemShow(answer0);
        }
    }

    private void SpecialDiaologue_ItemShow(string answer)
    {
        var dialog = new List<DialogData>();
        dialog.Add(new DialogData($"收到 {answer}，所以USER冒出來了", "", () => answerItem.SetActive(true)));

        DialogShow(dialog);
    }

    private void RightAnswerCloseButton(int iButtonIndex)
    {
        _buttonListSc.ButtonHide(iButtonIndex);
    }
}