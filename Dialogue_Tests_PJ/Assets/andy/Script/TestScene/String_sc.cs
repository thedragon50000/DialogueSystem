using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class String_sc : MonoBehaviour
{
    public string[] strArrayTemp;
    // public string[] strArrayTranformed;

    public TextAsset txt;
    public DialogManager DialogManager;

    //"內文&指令","角色名"

    //暫定角色名為國棟跟統神

    private void Awake()
    {
        strArrayTemp = txt.text.Split('\n');
    }

    void Start()
    {
        var temp = Add2DialogList(TxtTransform(strArrayTemp));

        // print(strArrayTemp[0].TrimEnd().TrimEnd(']')); //讀取文件時每一段換行都留有空白?先消除空白才能消除中括號


        DialogManager.Show(temp);
        // for (int i = 0; i < strArrayTemp.Length; i++)
        // {
        //     print(strArrayTemp[i]);
        // }
    }

    // Update is called once per frame
    void Update()
    {
    }

    string[,] TxtTransform(string[] strArray)
    {
        #region 先確定有幾個對話框

        int index = -1;

        for (int i = 0; i < strArray.Length; i++)
        {
            if (CheckSpeaker(strArray[i]) == E_Character.統神.ToString()
                || CheckSpeaker(strArray[i]) == E_Character.國棟.ToString())
            {
                print(CheckSpeaker(strArray[i]) + "是統神或國棟");
                index++;
            }
        }

        var temp = index + 1;
        print("共有幾個對話框:" + temp);

        string[,] str = new string[index + 1, 2]; //對話內容,說話者

        #endregion

        index = -1; //回收再利用

        #region 對話跟說話者都分類好了

        for (int i = 0; i < strArray.Length; i++)
        {
            if (CheckSpeaker(strArray[i]) == E_Character.統神.ToString()
                || CheckSpeaker(strArray[i]) == E_Character.國棟.ToString())
            {
                index++;
                str[index, 1] = CheckSpeaker(strArray[i]); //記住說話者
                print($"第{index}個對話的說話者:" + strArray[i]);
            }

            else if (CheckSpeaker(strArray[i]) != E_Character.統神.ToString()
                     && CheckSpeaker(strArray[i]) != E_Character.國棟.ToString())
            {
                print(strArray[i]);
                str[index, 0] += strArray[i]; //一段台詞
                print($"第{index}個對話的台詞:" + strArray[i]);
            }
        }

        SpeakerNameTransform(str);

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

    public void SpeakerNameTransform(string[,] s)
    {
        for (int i = 0; i < s.GetLength(0); i++)
        {
            if (s[i, 1] == E_Character.國棟.ToString())
            {
                s[i, 1] = "dong";
            }

            if (s[i, 1] == E_Character.統神.ToString())
            {
                s[i, 1] = "tong";
            }
        }
    }

    public DialogData Dialogue_Speaker(string dialog, string speaker)
    {
        return new DialogData(dialog, speaker);
    }
}

public enum E_Character
{
    統神,
    國棟
}