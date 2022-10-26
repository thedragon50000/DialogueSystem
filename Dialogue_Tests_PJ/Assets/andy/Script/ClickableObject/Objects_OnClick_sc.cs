using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Doublsb.Dialog;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Objects_OnClick_sc : baseOnClick
{
    public int iClickTimes = 0;

    [Tooltip("點超過幾次之後觸發特殊事件")] public int iSpecialTimes;

    [Tooltip("是否為第一次滑鼠經過")] public bool bFirstEnter = true;

    public override void OnMouseUp()
    {
        if (GameManager.IsTalking())
        {
            print("isTalking");
            return;
        }

        iClickTimes++;

        UnityAction act = null;
        act += GameManager.EnddingDialogue;
        act += () => act = null;

        // lstDialog = new List<DialogData>();
        lstDialog.Clear();
        if (iClickTimes >= iSpecialTimes)
        {
            SpecialDialog(lstDialog);
            ActionSet(lstDialog, 1, () => print("示範加action在第1句"));
        }
        else
        {
            lstDialog.Add(new DialogData("......", "Padko", act));
        }

        StartDialogIfNotTalking(lstDialog);
    }

    private void SpecialDialog(List<DialogData> dialogDatas)
    {
        int temp = iClickTimes + Random.Range(1, 5); //也可以兩個都是正解
        dialogDatas.Add(new DialogData("......", "Padko"));

        AddSelection(dialogDatas, "你知道你已經點了幾次了嗎？", E_Character.Padko.ToString()
            , new[] {temp.ToString(), iClickTimes.ToString()});
    }

    /// <summary>
    /// 對答案，不一定需要像原作者那樣訂一個key
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override void CheckSelectResult(List<DialogData> dialog, UnityAction action)
    {
        if (GameManager.DialogManager.Result == iClickTimes.ToString())
        {
            dialog.Add(new DialogData($"沒錯，你已經點{iClickTimes}次了", "Padko"));
            dialog.Add(new DialogData("別再點了(怒)", "Padko", action));
        }
        else
        {
            //bug: 選完選項的第一句，完全呼叫不到callback，不知道怎麼解                                                 ↓這個
            dialog.Add(new DialogData("腦袋壞掉", "Padko", () => GameManager.EnddingDialogue()));
            dialog.Add(new DialogData($"連自己點了{iClickTimes}次都不知道", "Padko", action));
        }
    }

    public void OnMouseEnter()
    {
        if (!bFirstEnter || GameManager.IsTalking())
        {
            return;
        }

        UnityAction act = () => bFirstEnter = false;
        act += GameManager.EnddingDialogue;
        act += () => act = null;

        lstDialog = new List<DialogData>();
        lstDialog.Add(new DialogData(
            Cmd.ChangeSpeed(0.9f) +
            "" +
            "走開啦", "Padko", act, false));

        // print("fuck off");
        GameManager.DialogueShow(lstDialog);
    }
}