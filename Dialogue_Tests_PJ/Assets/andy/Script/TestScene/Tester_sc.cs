using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
// using UnityEngine.UIElements;
using Zenject;

public class Tester_sc : MonoBehaviour
{
    [Inject] DialogManager dialogManager;
    public bool bIsTalking = false;
    public CommandManager cmd = new CommandManager();
    [Inject] private AudioManager audio;

    private void Update()
    {
        print("tester_Update()");
        if (Input.GetKeyUp(KeyCode.Q))
        {
            audio.PlayBGM("Lobby_Main");
        }
    }

    [Inject]
    public void Init()
    {
        print("dialogManager= " + dialogManager.name);
    }

    public void EnddingDialogue()
    {
        bIsTalking = false;
    }

    public void DialogueShow(List<DialogData> dialogTexts)
    {
        bIsTalking = true;
        dialogManager.Show(dialogTexts);
    }

    public bool IsTalking()
    {
        return bIsTalking;
    }
}