using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Zenject;

namespace Doublsb.Dialog
{
    public class DialogManager : MonoBehaviour
    {
        [Inject] private AudioManager audio;
        //================================================
        //Public Variable
        //================================================
        [Header("Game Objects")] public GameObject Printer;
        public GameObject Characters;

        [Header("UI Objects")] public Text Printer_Text;

        [Header("Audio Objects")] public AudioSource SEAudio;
        // public AudioSource CallAudio;    //Audio合併後不需要這個

        [Header("Preference")] public float Delay = 0.1f;

        [Header("Selector")] public GameObject Selector;
        public GameObject SelectorItem;
        public Text SelectorItemText;

        [HideInInspector] public E_State eState;

        /// <summary>
        /// 選完選項之後，Result會更改為對應的key
        /// </summary>
        [HideInInspector] public string Result;

        //================================================
        //Private Method
        //================================================
        private Character _current_Character;
        private DialogData _current_Data;

        /// <summary>
        /// 下一個字跟目前的字間隔幾秒印出
        /// skip就是讓_currentDelay =0 直到那句話講完
        /// </summary>
        private float _currentDelay;

        /// <summary>
        /// 記錄_currentDelay
        /// 不能skip的狀態就是
        /// 不斷讓_currentDelay = _lastDelay
        /// _currentDelay就永不為0且等速
        /// </summary>
        private float _tempDelay;

        private Coroutine _textingRoutine;
        private Coroutine _printingRoutine;

        //================================================
        //Public Method
        //================================================

        #region Show & Hide

        public void Show(DialogData Data)
        {
            _current_Data = Data;
            _find_character(Data.strCharacter);

            if (_current_Character != null)
                _emote("Normal");

            _textingRoutine = StartCoroutine(Activate());
        }

        public void Show(List<DialogData> Data)
        {
            StartCoroutine(Activate_List(Data));
        }

        //作者放了一個透明Button在對話框內，改掉
        //不想要的可以自己在程式內寫程式碼呼叫Click_Window()     20221013 HowWang modified
        //想留著也行，Printer內的Button屬性要勾

        public void Click_Window()
        {
            switch (eState)
            {
                case E_State.Active:
                    StartCoroutine(_skip());
                    break;

                case E_State.Wait:
                    if (_current_Data.SelectList.Count <= 0) Hide();
                    break;
            }
        }

        public void Hide()
        {
            if (_textingRoutine != null)
                StopCoroutine(_textingRoutine);

            if (_printingRoutine != null)
                StopCoroutine(_printingRoutine);

            Printer.SetActive(false);
            Characters.SetActive(false);
            Selector.SetActive(false);

            eState = E_State.Deactivate;

            //一段話結束時發動
            if (_current_Data.Callback != null)
            {
                _current_Data.Callback.Invoke();
                _current_Data.Callback = null;
            }
        }

        #endregion

        #region Selector

        public void Select(int index)
        {
            Result = _current_Data.SelectList.GetByIndex(index).Key;
            Hide();
        }

        #endregion

        #region Sound

        public void Play_ChatSE()
        {
            if (_current_Character != null)
            {
                SEAudio.clip = _current_Character.ChatSE[UnityEngine.Random.Range(0, _current_Character.ChatSE.Length)];
                SEAudio.Play();
            }
        }

        public void Play_CallSE(string SEname)
        {
            if (_current_Character != null)
            {
                // 理想狀態是決定好聲音並用Enum分類，而非這樣全用字串找
                
                // var FindSE
                //     = Array.Find(_current_Character.CallSE, (SE) => SE.name == SEname);
                //
                // CallAudio.clip = FindSE;
                // CallAudio.Play();

                // AudioManager.inst.PlaySFX(SEname); //20221008 HowWang add
                audio.PlaySFX(SEname); //20221008 HowWang add
            }
        }
        // public void Play_CallSE(Enum eVoice)
        // {
        //     if (_current_Character != null)
        //     {
        //         var FindSE
        //             = Array.Find(_current_Character.CallSE, (SE) => SE.name == eVoice.ToString());
        //
        //         CallAudio.clip = FindSE;
        //         CallAudio.Play();
        //
        //         AudioManager.inst.PlaySFX(E_PadkoVoice.haha.ToString());   //todo:Make it work
        //     }
        // }

        #endregion

        #region Speed

        public void Set_Speed(string speed)
        {
            switch (speed)
            {
                case "up":
                    _currentDelay -= 0.25f;
                    if (_currentDelay <= 0) _currentDelay = 0.001f;
                    break;

                case "down":
                    _currentDelay += 0.25f;
                    break;

                case "init":
                    _currentDelay = Delay;
                    break;

                default:
                    _currentDelay = float.Parse(speed);
                    break;
            }

            _tempDelay = _currentDelay;
        }

        #endregion

        //================================================
        //Private Method
        //================================================

        private void _find_character(string name)
        {
            if (name != string.Empty)
            {
                Transform Child = Characters.transform.Find(name);
                if (Child != null) _current_Character = Child.GetComponent<Character>();
            }
        }

        private void _initialize()
        {
            _currentDelay = Delay;
            _tempDelay = 0.1f;
            Printer_Text.text = string.Empty;

            Printer.SetActive(true);

            Characters.SetActive(_current_Character != null);
            foreach (Transform item in Characters.transform) item.gameObject.SetActive(false);
            if (_current_Character != null) _current_Character.gameObject.SetActive(true);
        }

        private void _init_selector()
        {
            _clear_selector();

            if (_current_Data.SelectList.Count > 0)
            {
                Selector.SetActive(true);

                for (int i = 0; i < _current_Data.SelectList.Count; i++)
                {
                    _add_selectorItem(i);
                }
            }

            else Selector.SetActive(false);
        }

        private void _clear_selector()
        {
            for (int i = 1; i < Selector.transform.childCount; i++)
            {
                Destroy(Selector.transform.GetChild(i).gameObject);
            }
        }

        private void _add_selectorItem(int index)
        {
            SelectorItemText.text = _current_Data.SelectList.GetByIndex(index).Value;

            var NewItem = Instantiate(SelectorItem, Selector.transform);
            NewItem.GetComponent<Button>().onClick.AddListener(() => Select(index));
            NewItem.SetActive(true);
        }

        #region Show Text

        private IEnumerator Activate_List(List<DialogData> DataList)
        {
            eState = E_State.Active;

            foreach (var Data in DataList)
            {
                Show(Data);
                _init_selector();

                while (eState != E_State.Deactivate)
                {
                    yield return null;
                }
            }
        }

        private IEnumerator Activate()
        {
            _initialize();

            eState = E_State.Active;

            foreach (var item in _current_Data.listCommands)
            {
                switch (item.ECommand)
                {
                    case E_Command.print:
                        yield return _printingRoutine = StartCoroutine(_print(item.Context));
                        break;

                    case E_Command.color:
                        _current_Data.Format.Color = item.Context;
                        break;

                    case E_Command.emote:
                        _emote(item.Context);
                        break;

                    case E_Command.size:
                        _current_Data.Format.Resize(item.Context);
                        break;

                    case E_Command.sound:
                        Play_CallSE(item.Context);
                        break;

                    case E_Command.speed:
                        Set_Speed(item.Context);
                        break;

                    case E_Command.click:
                        yield return _waitInput();
                        break;

                    case E_Command.close:
                        Hide();
                        yield break;

                    case E_Command.wait:
                        yield return new WaitForSeconds(float.Parse(item.Context));
                        break;
                }
            }

            eState = E_State.Wait;
        }

        private IEnumerator _waitInput()
        {
            while (!Input.GetMouseButtonDown(0)) yield return null;
            _currentDelay = _tempDelay;
        }

        private IEnumerator _print(string Text)
        {
            _current_Data.PrintText += _current_Data.Format.OpenTagger;

            for (int i = 0; i < Text.Length; i++)
            {
                _current_Data.PrintText += Text[i];
                Printer_Text.text = _current_Data.PrintText + _current_Data.Format.CloseTagger;

                if (Text[i] != ' ') Play_ChatSE();
                if (_currentDelay != 0) yield return new WaitForSeconds(_currentDelay);
            }

            _current_Data.PrintText += _current_Data.Format.CloseTagger;
        }

        public void _emote(string Text)
        {
            _current_Character.GetComponent<Image>().sprite = _current_Character.Emotion.Data[Text];
        }

        private IEnumerator _skip()
        {
            if (_current_Data.isSkippable)
            {
                _currentDelay = 0;
                while (eState != E_State.Wait) yield return null;
                _currentDelay = Delay;
            }
        }

        #endregion
    }
}