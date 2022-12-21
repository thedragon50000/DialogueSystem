using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

public class PuzzleManager_sc : MonoBehaviour
{
    [Inject(Id = "Level")] private int iLevel;

    int i
    {
        get => iLevel;
        set
        {
            iLevel = value;
            if (i >= Levels.Length)
            {
                value = 0;
                i = value;
                // i = 0;
            }
        }
    }

    [Inject] private ZenjectSceneLoader _sceneLoader;
    public Sprite[] Levels;

    public GameObject EndMenu;
    public IntReactiveProperty iPlacedPieces;

    private void Awake()
    {
    }

    void Start()
    {
        iPlacedPieces.Value = 0;
        iPlacedPieces.Subscribe(CheckProgression, Error, OnCompleted);

        var pieces = GetComponentsInChildren<piecesScript>();
        // print("是null嗎?" + pieces == null);
        foreach (var p in pieces)
        {
            //todo:換圖片，用scriptableObject
            var spriteRenderer = p.gameObject.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = Levels[iLevel];
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
        var s = E_ZenjectID.Level.ToString();
        i = iLevel + 1;
        print(i);
        _sceneLoader.LoadScene(1, LoadSceneMode.Single, container => container.BindInstance(i).WithId(s));
    }

    public void BacktoMenu()
    {
        _sceneLoader.LoadScene(0);
    }
}