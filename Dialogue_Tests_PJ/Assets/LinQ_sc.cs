using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinQ_sc : MonoBehaviour
{
    public TextAsset txt;
    public string s;

    void Start()
    {
        string strTemp = txt.text;

        // s = CutStr(strTemp, 50);

        s = CutStr("aabbccddeef", 2);
    }

    public string CutStr(string str, int len)
    {
        string s = "";

        for (int i = 0; i < 11; i++)
        {
            int r = i % 2;
            int last = 11 / 2 * 2;  //10
        }
        
        for (int i = 0; i < str.Length; i++)
        {
            int r = i % len;
            int last = (str.Length / len) * len;
            if (i != 0 && i <= last)
            {
                if (r == 0)
                {
                    s += str.Substring(i - len, len) + "\n";
                }
            }
            else if (i > last)
            {
                s += str.Substring(i - 1);
                break;
            }
        }

        return s;
    }
}