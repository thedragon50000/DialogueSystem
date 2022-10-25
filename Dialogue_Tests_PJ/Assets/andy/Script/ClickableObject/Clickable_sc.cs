using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;
using UnityEngine.Events;

public abstract class Clickable_sc : MonoBehaviour
{
    public Tester_sc GameManager;
    public CommandManager cmd = new CommandManager();
    private String_sc _txtReader;
    

    private void Awake()
    {
        _txtReader = gameObject.GetComponent<String_sc>();
    }

    public virtual void OnMouseUp()
    {
        if (GameManager.IsTalking())
        {
            return;
        }

        TxtTransform();
    }

    public List<DialogData> TxtTransform()
    {
        return _txtReader.Read_and_Transform();
    }

    void DialogueShow(List<DialogData> list)
    {
        GameManager.DialogueShow(list);
    }
}
