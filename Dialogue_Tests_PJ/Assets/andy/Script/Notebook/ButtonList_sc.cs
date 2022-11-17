using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonList_sc : MonoBehaviour
{
    public Button[] arrayButton;

    private void Awake()
    {
        arrayButton = new Button[transform.childCount];

        int iIndex = 0;
        foreach (RectTransform child in transform)
        {
            arrayButton[iIndex] = child.gameObject.GetComponent<Button>();
            iIndex++;
        }
    }

    void ButtonInit(int index)
    {
        E_WordPuzzleObj e = (E_WordPuzzleObj) index;
        string s = e.ToString();
        arrayButton[index].GetComponentInChildren<TMP_Text>().text = s;
        arrayButton[index].transform.SetAsLastSibling();
    }
}