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
            print("isTalking");
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
        int temp = iClickTimes + Random.Range(1, 5); //也可以兩個都是正解
        dialogDatas.Add(new DialogData("......", "Padko"));
        var Question1 = new DialogData("你知道你已經點了幾次了嗎?", "Padko");
        Question1.SelectList.Add(iClickTimes.ToString(), iClickTimes.ToString());
        Question1.SelectList.Add(temp.ToString(), temp.ToString());
        Question1.Callback = CheckAnswer;
        dialogDatas.Add(Question1);
        // dialogDatas.Add(new DialogData($"你已經點{iClickTimes}次了", "Padko", act));
    }

    /// <summary>
    /// 對答案，不一定需要像原作者那樣訂一個key
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private void CheckAnswer()
    {
        var dialog = new List<DialogData>();
        act += GameManager.EnddingDialogue;
        act += () => act = null;

        if (GameManager.DialogManager.Result == iClickTimes.ToString())
        {
            dialog.Add(new DialogData($"沒錯，你已經點{iClickTimes}次了", "Padko"));
            dialog.Add(new DialogData("別再點了(怒)", "Padko", act));
        }
        else
        {
            //bug: 選完選項的第一句，完全呼叫不到callback，不知道怎麼解                                                 ↓這個
            dialog.Add(new DialogData("腦袋壞掉", "Padko", () => GameManager.EnddingDialogue()));
            dialog.Add(new DialogData($"連自己點了{iClickTimes}次都不知道", "Padko", act));
        }

        GameManager.DialogueShow(dialog);
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

        act += () => bFirstEnter = false;
        act += GameManager.EnddingDialogue;
        act += () => act = null;

        var dialog = new List<DialogData>();
        dialog.Add(new DialogData(
            cmd.ChangeSpeed(0.9f) +
            "" +
            "走開啦", "Padko", act, false));

        // print("fuck off");
        GameManager.DialogueShow(dialog);
    }
}