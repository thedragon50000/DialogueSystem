using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Zenject;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class piecesScript : MonoBehaviour
{
    [Inject] private PuzzleManager_sc _puzzleManagerSc;

    public Vector3 v3RightPosition;

    public bool bInRightPosition;
    public bool bSelecting;

    private Image _imgPiece;

    public Vector3ReactiveProperty _v3PositionNow = new();

    public int iOrderInLayer = 0;

    private void Awake()
    {
        _imgPiece = gameObject.GetComponent<Image>();
        v3RightPosition = transform.position;
    }

    void Start()
    {
        transform.position = new Vector3(Random.Range(5f, 11f), Random.Range(2.5f, -7), Random.Range(-0.01f, -1f));

        // Observable.EveryUpdate().Subscribe(_ => PositionUpdate());

        _v3PositionNow.Subscribe(Check, Error, OnCompleted);


        this.OnMouseDownAsObservable() /*
            .SelectMany(_ => this.UpdateAsObservable())
            .TakeUntil(this.OnMouseUpAsObservable())
            .Select(_ => Input.mousePosition)*/.Subscribe(_ => { MouseDown(); });
        this.OnMouseUpAsObservable().Subscribe(_ => MouseUp());
        this.OnMouseDragAsObservable().Subscribe(_ => MouseDrag());
    }

    private void PositionUpdate()
    {
        _v3PositionNow.Value = transform.position;
    }

    private void MouseDrag()
    {
        print("OnMouseDrag()");
        if (!bSelecting) return;

        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var newPosition = new Vector3(mousePoint.x, mousePoint.y, -1);
        transform.position = newPosition;
    }

    private void MouseUp()
    {
        print("OnMouseUp()");
        bSelecting = false;

        var positionSelf = transform.position;
        var p = new Vector3(positionSelf.x, positionSelf.y, 0);
        _v3PositionNow.Value = p;
    }

    private void MouseDown()
    {
        print("OnMouseDown()");

        if (!bInRightPosition)
        {
            bSelecting = true;
            // GetComponent<SortingGroup>().sortingOrder = iOrderInLayer;
            // iOrderInLayer ++;
        }
    }

    private void OnCompleted()
    {
    }

    private void Error(Exception e)
    {
        Debug.LogError(e);
    }

    private void Check(Vector3 obj)
    {
        var v3 = obj;
        _v3PositionNow.Value = v3;
        // print($"Distance:{Vector3.Distance(_v3PositionNow.Value, v3RightPosition)}");

        print("bSelecting:" + bSelecting);
        // if (!bSelecting)
        // {
        if (Vector3.Distance(_v3PositionNow.Value, v3RightPosition) < 0.5f)
        {
            if (bInRightPosition == false)
            {
                transform.position = v3RightPosition;
                bInRightPosition = true;

                _puzzleManagerSc.iPlacedPieces.Value++;
            }
        }
        // }
    }
}