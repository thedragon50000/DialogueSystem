using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestString_sc : MonoBehaviour
{
    public string str = "";
    public string[] strings = {};
    public TextAsset txt;
    // Start is called before the first frame update
    void Start()
    {
        str = "/color:white//size:60//speed:0.1/全世界獨一無二只屬於user們的平板";
        strings = txt.text.Split('\n');

        // strings[0] = strings[0].Replace("\n", "");
        strings[0] = strings[0].TrimEnd();
        
        print("strings[0].Length" + strings[0].Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
