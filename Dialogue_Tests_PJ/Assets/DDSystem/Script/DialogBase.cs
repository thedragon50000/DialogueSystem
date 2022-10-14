using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Events;
using System.Linq;

namespace Doublsb.Dialog
{
    #region Enum

    /// <summary>
    /// 狀態
    /// </summary>
    public enum E_State
    {
        Active,
        /// <summary>
        ///等待點擊
        /// </summary>
        Wait,
        /// <summary>
        /// 
        /// </summary>
        Deactivate
    }

    /// <summary>
    /// 指令
    /// </summary>
    public enum E_Command   //todo:轉字串的方法
    {
        print,
        color,
        emote,
        size,
        sound,
        speed,
        click,
        close,
        wait
    }

    /// <summary>
    /// 字體顏色(字型不開放更改，以防自定義字體檔造成漏字)
    /// </summary>
    public enum E_TextColor
    {
        aqua,
        black,
        blue,
        brown,
        cyan,
        darkblue,
        fuchsia,
        green,
        grey,
        lightblue,
        lime,
        magenta,
        maroon,
        navy,
        olive,
        orange,
        purple,
        red,
        silver,
        teal,
        white,
        yellow
    }

    #endregion


    /// <summary>
    /// Convert string to Data. Contains List of DialogCommand and DialogFormat.
    /// 一個DialogData，是一組完整的角色立繪 + 對白 + 是否可以快轉對話的 bool
    /// UnityAction是一種Delegate,會在對話「結束」前觸發(如果有加的話)
    /// </summary>
    public class DialogData
    {
        //================================================
        //Public Variable
        //================================================
        public string strCharacter;
        public List<DialogCommand> listCommands = new List<DialogCommand>();

        public DialogSelect SelectList = new DialogSelect();
        public DialogFormat Format = new DialogFormat();

        public string PrintText = string.Empty;

        /// <summary>
        /// 是否可以快轉
        /// </summary>
        public bool isSkippable = true;

        public UnityAction Callback = null; //委派

        //================================================
        //Public Method
        //================================================


        public DialogData(string originalString, string strCharacter = "", UnityAction callback = null,
            bool isSkipable = true)
        {
            _convert(originalString);

            this.isSkippable = isSkipable;
            this.Callback = callback;
            this.strCharacter = strCharacter;
        }
        //================================================
        //Private Method
        //================================================
        private void _convert(string originalString)
        {
            string printText = string.Empty;

            for (int i = 0; i < originalString.Length; i++)
            {
                if (originalString[i] != '/') printText += originalString[i];

                else // If find '/'
                {
                    // Convert last printText to command
                    if (printText != string.Empty)
                    {
                        listCommands.Add(new DialogCommand(E_Command.print, printText));
                        printText = string.Empty;
                    }

                    // Substring /CommandSyntex/
                    var nextSlashIndex = originalString.IndexOf('/', i + 1);
                    string commandSyntex = originalString.Substring(i + 1, nextSlashIndex - i - 1);

                    // Add converted command
                    var com = _convert_Syntex_To_Command(commandSyntex);
                    if (com != null) listCommands.Add(com);

                    // Move i
                    i = nextSlashIndex;
                }
            }

            if (printText != string.Empty) listCommands.Add(new DialogCommand(E_Command.print, printText));
        }

        private DialogCommand _convert_Syntex_To_Command(string text)
        {
            var spliter = text.Split(':');

            E_Command eCommand;
            if (Enum.TryParse(spliter[0], out eCommand))
            {
                if (spliter.Length >= 2) return new DialogCommand(eCommand, spliter[1]);
                else return new DialogCommand(eCommand);
            }
            else
                Debug.LogError("Cannot parse to commands");

            return null;
        }
    }

    /// <summary>
    /// You can get RichText tagger of size and color.
    /// </summary>
    public class DialogFormat
    {
        //================================================
        //Private Variable
        //================================================
        public string DefaultSize = "60";
        private string _defaultColor = "white";

        private string _color;
        private string _size;


        //================================================
        //Public Method
        //================================================
        public DialogFormat(string defaultSize = "", string defaultColor = "")
        {
            _color = string.Empty;
            _size = string.Empty;

            if (defaultSize != string.Empty) DefaultSize = defaultSize;
            if (defaultColor != string.Empty) _defaultColor = defaultColor;
        }

        public string Color
        {
            set
            {
                if (isColorValid(value))
                {
                    _color = value;
                    if (_size == string.Empty) _size = DefaultSize;
                }
            }

            get => _color;
        }

        public string Size
        {
            set
            {
                if (isSizeValid(value))
                {
                    _size = value;
                    if (_color == string.Empty) _color = _defaultColor;
                }
            }

            get => _size;
        }

        public string OpenTagger
        {
            get
            {
                if (isValid) return $"<color={Color}><size={Size}>";
                else return string.Empty;
            }
        }

        public string CloseTagger
        {
            get
            {
                if (isValid) return "</size></color>";
                else return string.Empty;
            }
        }

        public void Resize(string command)
        {
            if (_size == string.Empty) Size = DefaultSize;

            switch (command)
            {
                case "up":
                    _size = (int.Parse(_size) + 10).ToString();
                    break;

                case "down":
                    _size = (int.Parse(_size) - 10).ToString();
                    break;

                case "init":
                    _size = DefaultSize;
                    break;

                default:
                    _size = command;
                    break;
            }
        }

        //================================================
        //Private Method
        //================================================
        private bool isValid
        {
            get => _color != string.Empty && _size != string.Empty;
        }

        private bool isColorValid(string Color)
        {
            E_TextColor eTextColor;
            Regex hexColor = new Regex("^#(?:[0-9a-fA-F]{3}){1,2}$");

            return Enum.TryParse(Color, out eTextColor) || hexColor.Match(Color).Success;
        }

        private bool isSizeValid(string Size)
        {
            float size;
            return float.TryParse(Size, out size);
        }
    }

    public class DialogCommand
    {
        public E_Command ECommand;
        public string Context;

        public DialogCommand(E_Command eCommand, string context = "")
        {
            ECommand = eCommand;
            Context = context;
        }
    }

    public class DialogSelect
    {
        private List<DialogSelectItem> ItemList;

        public DialogSelect()
        {
            ItemList = new List<DialogSelectItem>();
        }

        public int Count
        {
            get => ItemList.Count;
        }

        public DialogSelectItem GetByIndex(int index)
        {
            return ItemList[index];
        }

        public List<DialogSelectItem> Get_List()
        {
            return ItemList;
        }

        public string Get_Value(string Key)
        {
            return ItemList.Find((var) => var.isSameKey(Key)).Value;
        }

        public void Clear()
        {
            ItemList.Clear();
        }

        public void Add(string Key, string Value)
        {
            ItemList.Add(new DialogSelectItem(Key, Value));
        }

        public void Remove(string Key)
        {
            var item = ItemList.Find((var) => var.isSameKey(Key));

            if (item != null) ItemList.Remove(item);
        }
    }

    public class DialogSelectItem
    {
        public string Key;
        public string Value;

        public bool isSameKey(string key)
        {
            return Key == key;
        }

        public DialogSelectItem(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}