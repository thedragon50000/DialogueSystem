using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageList_sc : MonoBehaviour
{
    public WordImage_sc[] arrayImg;
    public int iIndex;

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