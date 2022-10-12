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
            temp = string.Format("/color:{0}/",sColor);
            return temp;
        }

        public string ChangeSize()
        {
            string temp = "";
            return temp;
        }
    }

    /*
     /color:red/

/size:up/

/size:init/

/size:down/

     /emote:Happy/
     
     /click/

/wait:0.1/

/speed:down/

/speed:up/

/speed:init/

/speed:0.2/

/close/

bool isSkipable = false

/sound:haha/
     */
}