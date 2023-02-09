using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestBuildButton : MonoBehaviour
{
    public Char[] strAllLetters;
    public Button btnCrossWordsLetter;
    public TMP_Text[] Texts;
    public Button[] buttons;

    

    // Start is called before the first frame update
    void Start()
    {
        // Buttons = GetComponentsInChildren<Button>();
        // Texts = gameObject.GetComponentsInChildren<TMP_Text>();

        // for (int i = 0; i < Buttons.Length; i++)
        // {
        //     Buttons[i].gameObject.name = Texts[i].text;
        // }

        // strAllLetters =
        //     "過期美食美索不達米亞平原不可以瑟瑟作息破壞達人全婆俠電死平板電腦破腦改管衝蝦啦炸蝦抱枕每日彩蛋英日文小教室小手拍手作禮物信件拍肚肚初配信回顧衝瞎虫顧問初次事故小小夢魘禮物聚集地地瓜球開心的表情貓型生物心型氣球氣氛燈抒情音樂樂隊禮砲"
        //         .ToCharArray();
        // foreach (var letter in strAllLetters)
        // {
        //     Button button = Instantiate(btnCrossWordsLetter, transform);
        //     var text = button.GetComponentInChildren<TMP_Text>();
        //     text.text = letter.ToString();
        //     button.gameObject.SetActive(false);
        // }


        for (int i = 0; i < 5; i++)
        {
            FindButtonByText_NotRepeating("小");
        }

        FindButtonByText_NotRepeating("平板電腦");
    }

    void FindButtonByText_NotRepeating(string s)
    {
        //  = 
        foreach (char c in s)
        {
            foreach (var t in Texts)
            {
                // var bGameObject = b.gameObject;
                // if (t.text == s)
                if (t.text == c.ToString())
                {
                    var parentGameObject = t.transform.parent.gameObject;
                    if (parentGameObject.activeSelf == false)
                    {
                        parentGameObject.SetActive(true);
                        break;
                    }
                }
            }
        }
    }
}