using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.Assertions;
using UnityEngine.Events;
using System.Linq;

public class String_sc : baseString
{
    // 假設我今天想要讓同一個對象講兩種以上的文本，那就多宣告幾份，txt檔分開放
    // 其他處理都一樣，分段之類的判斷交給OnClick那邊的腳本

    /*
    public string[] strSpecialArrayTemp;
    public TextAsset txtSpecial;
    
    */

}

public enum E_Character
{
    tong,
    dong,
    Me,
    Padko,
    MAX
}