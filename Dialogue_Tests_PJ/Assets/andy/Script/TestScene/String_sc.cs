using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.Assertions;
using UnityEngine.Events;
using System.Linq;

public class String_sc : MonoBehaviour
{
    public string[] strArrayTemp;
    // public string[] strArrayTranformed;

    public Tester_sc GameManager;
    public TextAsset txt;


    //"內文&指令","角色名"

    private void Awake()
    {
        if (txt != null)
        {
            strArrayTemp = txt.text.Split('\n');
        }
    }

    public List<DialogData> Read_and_Transform()
    {
        var temp = Add2DialogList(TxtTransform(strArrayTemp));

        // print(strArrayTemp[0].TrimEnd().TrimEnd(']')); //讀取文件時每一段換行都留有空白?先消除空白才能消除中括號

        ActionAdd(temp, temp.Count, GameManager.EnddingDialogue);

        // ActionAdd(temp, new[] {1, 2}, new UnityAction[] {() => print("添加action1"), () => print("添加action2")});

        //只讀不播放
        // GameManager.DialogueShow(temp);

        return temp;
    }

    #region 若有需要加Action，再另外寫一個腳本額外添加進去

    public void ActionAdd(List<DialogData> temp, int[] iDialogAction, UnityAction[] actions)
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

    public void ActionAdd(List<DialogData> temp, int iDialogAction, UnityAction action)
    {
        if (iDialogAction > temp.Count)
        {
            print("錯誤，指定的位置超出對話總數");
            return;
        }

        temp[iDialogAction - 1].Callback = action;
    }

    #endregion


    string[,] TxtTransform(string[] strArray)
    {
        #region 先確定有幾個對話框

        int index = -1;

        for (int i = 0; i < strArray.Length; i++)
        {
            if (bCheckSpeakers(CheckSpeaker(strArray[i])))
            {
                print(CheckSpeaker(strArray[i]) + "是一個段落");
                index++;
            }
        }

        var temp = index + 1;
        print("共有幾個對話框:" + temp);

        string[,] str = new string[temp, 2]; //對話內容,說話者

        #endregion

        index = -1; //回收再利用

        #region 對話跟說話者都分類好了

        for (int i = 0; i < strArray.Length; i++) //i是第幾行
        {
            if (bCheckSpeakers(CheckSpeaker(strArray[i])))
            {
                index++;
                str[index, 1] = CheckSpeaker(strArray[i]);
                print($"第{index}個對話的說話者:" + strArray[i]);
            }
            else if (!bCheckSpeakers(CheckSpeaker(strArray[i])))
            {
                // print(strArray[i]);
                str[index, 0] += strArray[i]; //一段台詞
                print($"第{index}個對話的台詞:" + strArray[i]);
            }
        }

        // SpeakerNameTransform(str);

        #endregion

        return str;
    }

    private string CheckSpeaker(string str)
    {
        string temp = str;
        return temp.Trim('[').TrimEnd().TrimEnd(']');
    }

    private List<DialogData> Add2DialogList(string[,] str)
    {
        List<DialogData> dialog = new List<DialogData>();
        for (int i = 0; i < str.GetLength(0); i++)
        {
            // print(str[i, 0] + '\n' + str[i, 1]);
            dialog.Add(Dialogue_Speaker(str[i, 0], str[i, 1]));
        }

        return dialog;
    }


    public DialogData Dialogue_Speaker(string dialog, string speaker)
    {
        return new DialogData(dialog, speaker);
    }

    bool bCheckSpeakers(string speaker)
    {
        for (int i = 0; i < (int) E_Character.MAX; i++)
        {
            E_Character temp = (E_Character) i;
            if (speaker == temp.ToString())
            {
                return true;
            }
        }

        return false;
    }
}

public enum E_Character
{
    tong,
    dong,
    Me,
    Padko,
    MAX
}