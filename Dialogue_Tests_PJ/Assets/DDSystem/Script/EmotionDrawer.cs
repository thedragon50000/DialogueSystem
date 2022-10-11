#if UNITY_EDITOR
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace Doublsb.Dialog
{
    [CustomPropertyDrawer(typeof(Emotion))]
    public class EmotionDrawer : PropertyDrawer
    {
        #region Private Variables

        //================================================
        //Private Variable
        //================================================
        private int iArraySize; //總共有幾個表情

        private SerializedProperty _emotion;
        private SerializedProperty _sprite;
        private string EmotionName = "Input the emotion name";

        #endregion

        #region Events

        //================================================
        //Public Method
        //================================================

        /// <summary>
        ///
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label">標籤</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = 18f; //設定單行高度18
            // position.y += position.height; //往下移一行

            EditorGUI.BeginProperty(position, label, property);

            _initialize(position, property);
            _display_Header(position);
            position.y += position.height; //往下移一行
            _display_EmotionList(position);
            _display_AddArea(position);

            EditorGUI.EndProperty();
            // Debug.Log(position);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     動態調整GUI的高度
        /// </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 18 * (iArraySize + 4);
        }

        #endregion

        #region init

        //================================================
        //Private Method : init
        //================================================
        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="property"></param>
        private void _initialize(Rect pos, SerializedProperty property)
        {
            _emotion = property.FindPropertyRelative("ArrayEmotion");
            _sprite = property.FindPropertyRelative("ArraySprite");

            iArraySize = _emotion.arraySize;
        }

        #endregion

        #region Private Methods

        private void _add_Raw()
        {
            if (!_is_duplicated_emotion_name(EmotionName))
            {
                _emotion.InsertArrayElementAtIndex(_emotion.arraySize);
                _emotion.GetArrayElementAtIndex(_emotion.arraySize - 1).stringValue = EmotionName;

                _sprite.InsertArrayElementAtIndex(_sprite.arraySize);
            }
        }

        //================================================
        //Private Method : methods
        //================================================
        private void _delete_ArrayElement(SerializedProperty array, int index, bool isObject = false)
        {
            //objectReferenceValue才會有圖片名稱 bugfixed 20221007 HowWang
            if (isObject &&
                // array.GetArrayElementAtIndex(index) != null  //這個不行，永遠不會是null
                array.GetArrayElementAtIndex(index).objectReferenceValue != null) //若圖片陣列內有東西
            {
                Debug.Log("圖片非空");

                #region 沒有用處的程式碼

                // Debug.Log(array.GetArrayElementAtIndex(index).displayName);
                // Debug.Log(array.GetArrayElementAtIndex(index).stringValue);
                // Debug.Log(array.GetArrayElementAtIndex(index).ToString());
                // Debug.Log(array.GetArrayElementAtIndex(index).name);
                // Debug.Log(array.GetArrayElementAtIndex(index).GetType());
                // Debug.Log(array.GetArrayElementAtIndex(index).propertyType);
                // Debug.Log(array.GetArrayElementAtIndex(index).arrayElementType);

                #endregion

                Debug.Log(array.GetArrayElementAtIndex(index).objectReferenceValue);

                array.DeleteArrayElementAtIndex(index); //清除該圖片

                array.DeleteArrayElementAtIndex(index); //刪除格子
            }
            else
            {
                array.DeleteArrayElementAtIndex(index);
            }
        }

        private void _delete_Raw(int index)
        {
            _delete_ArrayElement(_emotion, index);
            _delete_ArrayElement(_sprite, index, true);
        }

        private void _display_AddArea(Rect startPos)
        {
            ///(From.position + new Vector2(x, y), new Vector2(width, 16))
            var InputRect = _get_new_Rect(startPos, 0, startPos.width / 3 * 2, (_emotion.arraySize + 1) * 18);
            Debug.Log("_emotion.arraySize" + _emotion.arraySize);
            _display_TextArea(InputRect);
            _display_AddButton(_get_new_Rect(InputRect, InputRect.width + 20, 70));
        }

        private void _display_AddButton(Rect rect)
        {
            if (GUI.Button(rect, "create"))
            {
                _add_Raw();
                EmotionName = "";
            }
        }

        private void _display_Array(Rect startPos, SerializedProperty array)
        {
            for (var i = 0; i < array.arraySize; i++)
            {
                startPos = new Rect(startPos.position + new Vector2(0, 18), startPos.size);
                EditorGUI.PropertyField(startPos, array.GetArrayElementAtIndex(i), GUIContent.none);
            }
        }

        private void _display_DeleteButton(Rect startPos)
        {
            for (var i = 0; i < _sprite.arraySize; i++)
            {
                startPos = new Rect(startPos.position + new Vector2(0, 18), startPos.size);
                if (_emotion.GetArrayElementAtIndex(i).stringValue != "Normal" && GUI.Button(startPos, "-"))
                {
                    var j = i;
                    _delete_Raw(j);
                }
            }
        }

        private void _display_EmotionList(Rect startPos)
        {
            var NewRect = new Rect(startPos.position, new Vector2(startPos.width / 3, 16));

            _display_Array(NewRect, _emotion);
            _display_Array(_get_new_Rect(NewRect, NewRect.width, NewRect.width), _sprite);
            _display_DeleteButton(_get_new_Rect(NewRect, NewRect.width * 2 + 10, 30));
        }

        //================================================
        //Private Method : display
        //================================================
        private void _display_Header(Rect startPos)
        {
            EditorGUI.LabelField(startPos, "Emotion");
            EditorGUI.indentLevel++;
        }

        private void _display_TextArea(Rect rect)
        {
            EmotionName = EditorGUI.TextField(rect, EmotionName);
        }

        private Rect _get_new_Rect(Rect From, float x, float width)
        {
            return new Rect(From.position + new Vector2(x, 0), new Vector2(width, 16));
        }

        private Rect _get_new_Rect(Rect From, float x, float width, float y)
        {
            return new Rect(From.position + new Vector2(x, y), new Vector2(width, 16));
        }

        #endregion


        /// <summary>
        ///     檢查重複
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool _is_duplicated_emotion_name(string name)
        {
            for (var i = 0; i < _emotion.arraySize; i++)
                if (_emotion.GetArrayElementAtIndex(i).stringValue == name)
                    return true;

            return false;
        }
    }
}
#endif