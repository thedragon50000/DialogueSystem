using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindSameUnit_sc : MonoBehaviour
{
    private string _unnecessaryPart1 = "僅平情待超味著糕香味俱掉爆於屢爆成越越晚管見不怪性息壞人時間到子意刺大中入人炸明精事起啦當轟五去凡好袋太一致燈頭平子木棕物";
    private string _unnecessaryPart2 = "外徑順利姿我們用答疑滅跳耳態盼這吃";
    private string _answers = "過期美食美索不達米亞平原不可以色色作息破壞達人全婆俠電死平板電腦破腦改管衝蝦啦炸蝦抱枕每日彩蛋英日文小教室小手拍手作禮物信件拍肚肚初配信回顧衝瞎虫顧問初次事故小小夢魘禮物聚集地地瓜球開心的表情貓型生物心型氣球氣氛燈抒情音樂樂隊禮砲";
    
    // Start is called before the first frame update
    void Start()
    {
        IEnumerable<char> chars = _unnecessaryPart1.Intersect(_unnecessaryPart2);
        foreach (char c in chars)
        {
            print(c);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
