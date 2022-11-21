using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WordImage_sc : MonoBehaviour
{
    public int iIndex;
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    public void ImageInit()
    {
        E_WordPuzzleObj temp = (E_WordPuzzleObj) iIndex;
        _text.text = temp.ToString();
        gameObject.transform.SetAsLastSibling();
    }

    public void ImageDisapaer()
    {
        _text.text = string.Empty;
    }
}

public enum E_WordPuzzleObj
{
    我,
    喜歡,
    USER,
    D,
    E,
}