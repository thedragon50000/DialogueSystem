using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
// using UnityEngine.UIElements;
using UnityEngine.UI;

public class Tester_sc : MonoBehaviour
{
    public DialogManager DialogManager;
    public bool bIsTalking = false;
    public CommandManager cmd = new CommandManager();

    public void EnddingDialogue()
    {
        bIsTalking = false;
    }

    public void DialogueShow(List<DialogData> dialogTexts)
    {
        bIsTalking = true;
        DialogManager.Show(dialogTexts);
    }

    public bool IsTalking()
    {
        return bIsTalking;
    }
}