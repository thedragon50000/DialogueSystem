using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Doublsb.Dialog
{
    //所有的commands放這裡，不要再把字串混在對白內

    public class CommandManager
    {
        public string ChangeColor(E_TextColor _color)
        {
            string temp = "";
            string sColor = _color.ToString();
            temp = $"/color:{sColor}/";
            return temp;
        }

        public string ChangeSize(E_Up_Down_Init upDownInit)
        {
            string temp = "";
            string sSize = upDownInit.ToString();
            temp = $"/size:{sSize}/";
            return temp;
        }

        /// <summary>
        /// 要先把表情整理成enum
        /// </summary>
        /// <param name="emotion"></param>
        /// <returns></returns>
        public string ChangeEmotion(string emotion)
        {
            string temp = "";
            temp = $"/emote:{emotion}/";
            return temp;
        }

        public string WaitingClick()
        {
            string temp = "/click/";

            return temp;
        }

        public string Wait_for_Seconds(float second)
        {
            string temp = "";
            temp = $"/wait:{second}/";

            return temp;
        }

        public string ChangeSpeed(E_Up_Down_Init eUpDownInit)
        {
            string temp = "";
            string sSpeed = eUpDownInit.ToString();
            temp = $"/speed:{sSpeed}/";

            return temp;
        }

        public string ChangeSpeed(float speed)
        {
            string temp = "";
            temp = $"/speed:{speed}/";

            return temp;
        }

        public string Close()
        {
            string temp = "";
            temp = "/close/";

            return temp;
        }

        public string NewLine()
        {
            return "\n";
        }

        /// <summary>
        /// 先把音效整理成enum
        /// </summary>
        /// <param name="sound"></param>
        /// <returns></returns>
        public string PlaySound(string sound)
        {
            string temp = "";
            temp = $"/sound:{sound}/";

            return temp;
        }
    }

    /*
     /emote:Happy/
     
bool isSkipable = false

     */
}

public enum E_Up_Down_Init
{
    up,
    down,
    init
}