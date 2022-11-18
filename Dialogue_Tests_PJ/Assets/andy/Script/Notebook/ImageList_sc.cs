using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ImageList_sc : MonoBehaviour
{
    public WordImage_sc[] arrayImg;
    public int iIndex;
    [Inject] private StringManager_sc stringManager;

    private void Awake()
    {
        arrayImg = new WordImage_sc[transform.childCount];
        int index = 0;
        foreach (RectTransform child in transform)
        {
            // print("child type = " + child.GetType());
            arrayImg[index] = child.gameObject.GetComponent<WordImage_sc>();
            index++;
        }
    }

    public void ImageInitByIndex(int i)
    {
        arrayImg[i].gameObject.SetActive(true);
        arrayImg[i].ImageInit();
        E_WordPuzzleObj eTemp = (E_WordPuzzleObj) i;
        stringManager.strAnswer.Value += eTemp.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ImageDisapearByIndex(int i)
    {
        arrayImg[i].ImageDisapaer();
        arrayImg[i].gameObject.SetActive(false);
    }
}