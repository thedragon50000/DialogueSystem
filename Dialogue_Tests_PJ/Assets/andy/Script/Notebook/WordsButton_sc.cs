using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WordsButton_sc : MonoBehaviour
{
    private Button btn;

    public BoolReactiveProperty isNotUsedYet; //是否已經用掉了
    public BoolReactiveProperty isUsing; //正在使用中，但還沒用掉

    public ImageList_sc imageList;

    public int iIndex; //對應底下圖片編號用
    TMP_Text _text; //文字的圖片

    private void Awake()
    {
        imageList = FindObjectOfType<ImageList_sc>();
        btn = gameObject.GetComponent<Button>();
        _text = GetComponentInChildren<TMP_Text>();
        isNotUsedYet.Value = true;
        isUsing.Value = false;
    }

    void Start()
    {
        isNotUsedYet.Subscribe(NotUsedYet, Error, Complete); //如果已經用掉了就直接關閉該按鈕
        isUsing.Subscribe(Using, Error, Complete);

        btn.OnClickAsObservable().Subscribe(_ =>
        {
            //ShowText and Put it to the last
            ShowText(isUsing.Value);
        });
    }

    private void Using(bool obj)
    {
        bool temp = obj;
        if (temp)
        {
            _text.color = Color.gray;
        }

        if (!temp)
        {
            _text.color = Color.black;
        }
    }

    private void Complete()
    {
        print("Completed");
    }

    private void Error(Exception e)
    {
        print($"ERROR:{e}");
    }

    private void NotUsedYet(bool obj)
    {
        bool temp = obj;
        gameObject.SetActive(temp);
    }

    private void ShowText(bool b)
    {
        bool bTemp = b;
        ImgInitialize(iIndex, bTemp);
        isUsing.Value = !b;
    }

    private void ImgInitialize(int i, bool bTemp)
    {
        if (bTemp)
        {
            imageList.ImageDisapearByIndex(i);
            return;
        }
        
        imageList.ImageInitByIndex(i);
        //呼叫對應編號的圖片並插到最後面
        //關閉已經開啟的對應編號圖片
    }


    // Update is called once per frame
    void Update()
    {
    }
}