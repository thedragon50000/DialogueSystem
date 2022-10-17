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

    // Start is called before the first frame update
    void Start()
    {
        // var dd = new List<DialogData>();
        // dd.Add(new DialogData( "test","",EnddingDialogue));
        // DialogueShow(dd);
    }

    // Update is called once per frame
    void Update()
    {
    }


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