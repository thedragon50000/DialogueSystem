using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;
using UnityEngine.Events;

public abstract class Clickable_sc : MonoBehaviour
{
    public Tester_sc GameManager;
    private CommandManager cmd = new CommandManager();
    String_sc _txtReader;

    public virtual void Awake()
    {
        _txtReader = gameObject.GetComponent<String_sc>();
    }

    public virtual void OnMouseUp()
    {
        StartDialogIfNotTalking();
    }

    private void StartDialogIfNotTalking()
    {
        if (GameManager.IsTalking())
        {
            return;
        }

        DialogueShow();
    }

    public List<DialogData> TxtTransform()
    {
        return _txtReader.Read_and_Transform();
    }

    protected void DialogueShow()
    {
        GameManager.DialogueShow(TxtTransform());
    }
}