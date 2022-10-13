using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Doublsb.Dialog
{
    [RequireComponent(typeof(Image))]
    public class Character : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        ///     叫聲等音效，指定時間播放
        /// </summary>
        public AudioClip[] CallSE;

        /// <summary>
        ///     講話時的嘟嘟聲，對話時不斷播放 todo:暫時不考慮合併，不想每次講一段話就呼叫十幾次AudioManager
        /// </summary>
        public AudioClip[] ChatSE;

        /// <summary>
        ///     表情
        /// </summary>
        public Emotion Emotion;

        #endregion
    }

    //將Emotion從DialogueBase移到Character  HowWang 20221007
    #region Emotion
    [Serializable]
    public class Emotion
    {
        //================================================
        //Public Variable
        //================================================
        private Dictionary<string, Sprite> _data;
        public Dictionary<string, Sprite> Data
        {
            get
            {
                if (_data == null) _init_emotionList();
                return _data;
            }
        }

        public string[] ArrayEmotion = new string[] { "Normal" };
        // public Sprite[] _sprite ;
        public Sprite[] ArraySprite = new Sprite[1];    //20221007 HowWang

        //================================================
        //Private Method
        //================================================
        private void _init_emotionList()
        {
            _data = new Dictionary<string, Sprite>();

            if (ArrayEmotion.Length != ArraySprite.Length)
                Debug.LogError("Emotion and Sprite have different lengths");

            for (int i = 0; i < ArrayEmotion.Length; i++)
                _data.Add(ArrayEmotion[i], ArraySprite[i]);
        }
    }
    #endregion
}