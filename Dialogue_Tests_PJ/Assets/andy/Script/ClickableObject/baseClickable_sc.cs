﻿using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;
using UnityEngine.Events;

public abstract class baseClickable_sc : MonoBehaviour
{
    public Tester_sc GameManager;
    protected readonly CommandManager Cmd = new CommandManager();
    public baseString _txtReader;
    public List<DialogData> lstDialog;
    protected void Awake()
    {
        _txtReader = gameObject.GetComponent<String_sc>();
    }
    
    public virtual void OnMouseUp()
    {
        lstDialog = _txtReader.Read_and_Transform();

        StartDialogIfNotTalking(lstDialog);
    }

    protected void StartDialogIfNotTalking(List<DialogData> dialog)
    {
        if (GameManager.IsTalking())
        {
            return;
        }

        GameManager.DialogueShow(dialog);
    }

    protected void ActionSet(List<DialogData> temp, int[] iDialogAction, UnityAction[] actions)
    {
        int itemp = -1;

        foreach (int i in iDialogAction)
        {
            if (i > temp.Count)
            {
                print("錯誤，指定的位置超出對話總數");
                return;
            }
        }

        foreach (int i in iDialogAction)
        {
            itemp++;
            temp[i - 1].Callback = actions[itemp];
        }
    }

    protected void ActionSet(List<DialogData> temp, int iDialogAction, UnityAction action)
    {
        if (iDialogAction > temp.Count)
        {
            print("錯誤，指定的位置超出對話總數");
            return;
        }

        //注意！這是覆寫
        temp[iDialogAction - 1].Callback = action;
    }

    /// <summary>
    /// 對答案時需要key
    /// </summary>
    /// <param name="dialogDatas"></param>
    /// <param name="question"></param>
    /// <param name="speaker"></param>
    /// <param name="selections"></param>
    /// <param name="selectionkeys"></param>
    protected void AddSelection(List<DialogData> dialogDatas, string question, string speaker, string[] selections,
        string[] selectionkeys)
    {
        var Question1 = new DialogData(question, "Padko");
        for (int i = 0; i < selections.Length; i++)
        {
            Question1.SelectList.Add(selectionkeys[i], selections[i]);
        }

        Question1.Callback = AfterSelect;
        dialogDatas.Add(Question1);
    }

    /// <summary>
    /// 對答案時不用key
    /// </summary>
    /// <param name="dialogDatas"></param>
    /// <param name="question"></param>
    /// <param name="speaker"></param>
    /// <param name="selections"></param>
    protected void AddSelection(List<DialogData> dialogDatas, string question, string speaker, string[] selections)
    {
        var Question1 = new DialogData(question, "Padko");
        for (int i = 0; i < selections.Length; i++)
        {
            Question1.SelectList.Add(selections[i], selections[i]);
        }

        Question1.Callback = AfterSelect;
        dialogDatas.Add(Question1);
    }

    private void AfterSelect()
    {
        var dialog = new List<DialogData>();
        UnityAction act = GameManager.EnddingDialogue;
        act += () => act = null;

        CheckSelectResult(dialog, act);

        GameManager.DialogueShow(dialog);
    }

    protected virtual void CheckSelectResult(List<DialogData> dialog, UnityAction action)
    {
        
    }
}