using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class NormalCharacterOnClick_sc : MonoBehaviour
{
    public Tester_sc GameManager;

    private String_sc txtReader;

    private void Awake()
    {
        txtReader = gameObject.GetComponent<String_sc>();
    }

    public void OnMouseUp()
    {
        if (GameManager.IsTalking())
        {
            print("isTalking");
            return;
        }

        txtReader.Read_and_Transform();

        GameManager.DialogueShow(txtReader.Read_and_Transform());
    }
}