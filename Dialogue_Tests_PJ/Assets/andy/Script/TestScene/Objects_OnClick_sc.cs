using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Doublsb.Dialog;
using UnityEngine.Events;

public class Objects_OnClick_sc : MonoBehaviour
{
    public int iClickTimes = 0;

    public Tester_sc GameManager;
    public CommandManager cmd = new CommandManager();

    [Tooltip("點超過幾次之後觸發特殊事件")] public int iSpecialTimes;

    [Tooltip("是否為第一次滑鼠經過")] public bool bFirstEnter = true;

    public UnityAction act = null;

    public void OnMouseUp()
    {
        if (GameManager.IsTalking())
        {
            return;
        }
        iClickTimes++;

        act += GameManager.EnddingDialogue;
        act += () => act = null;

        var dialog = new List<DialogData>();
        if (iClickTimes >= iSpecialTimes)
        {
            SpecialDialog(dialog);
        }
        else
        {
            dialog.Add(new DialogData("......", "Padko", act));
        }

        GameManager.DialogueShow(dialog);
    }

    private void SpecialDialog(List<DialogData> dialogDatas)
    {
        dialogDatas.Add(new DialogData("......", "Padko"));
        dialogDatas.Add(new DialogData($"你已經點{iClickTimes}次了", "Padko", act));
    }

    void EndDialogue()
    {
        GameManager.EnddingDialogue();
        iClickTimes++;
    }

    public void OnMouseEnter()
    {
        if (!bFirstEnter || GameManager.IsTalking())
        {
            return;
        }

        act = null;
        act += () => bFirstEnter = false;
        act += GameManager.EnddingDialogue;

        var dialog = new List<DialogData>();
        dialog.Add(new DialogData(
            cmd.ChangeSpeed(0.9f) +
            "" +
            "走開啦", "Padko", act, false));

        // print("fuck off");
        GameManager.DialogueShow(dialog);
    }
}