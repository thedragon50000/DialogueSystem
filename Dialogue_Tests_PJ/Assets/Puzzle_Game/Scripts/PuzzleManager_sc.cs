using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

public class PuzzleManager_sc : MonoBehaviour
{
    private int iLevel;
    public Sprite[] Levels;

    public GameObject EndMenu;
    public IntReactiveProperty iPlacedPieces;

    private void Awake()
    {
        // iLevel = _puzzleLevel.iLevel;
    }

    void Start()
    {
        iPlacedPieces.Value = 0;
        iPlacedPieces.Subscribe(CheckProgression, Error, OnCompleted);

        var pieces = GetComponentsInChildren<piecesScript>();
        print("是null嗎?" + pieces == null);
        foreach (var p in pieces)
        {
            //todo:換圖片，用scriptableObject
            var spriteRenderer = p.gameObject.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = Levels[PlayerPrefs.GetInt("Level")];
        }
    }

    private void OnCompleted()
    {
    }

    private void Error(Exception e)
    {
        Debug.LogError(e);
    }

    private void CheckProgression(int obj)
    {
        var i = obj;
        iPlacedPieces.Value = i;

        if (i == 36)
        {
            EndMenu.SetActive(true);
        }
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene("Game");
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}