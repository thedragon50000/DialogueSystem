using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using Unity.VisualScripting;
using Sequence = DG.Tweening.Sequence;

public class Cinematic_sc : MonoBehaviour
{
    private Sequence _sequence;
    private float _fDefaultCameraViewField;
    private Sequence _doSequence;

    public Vector3[] v3Positions;
    public GameObject[] goPositions;
    public int iProgress;
    public float fCameraX;

    void ChangeSite()
    {
        if (iProgress < v3Positions.Length)
        {
            transform.DOLookAt(v3Positions[iProgress], .5f, AxisConstraint.W);
            print("進度:" + iProgress);
            iProgress++;
        }
        else
        {
            print("走到了盡頭");
        }
    }

    private void Awake()
    {
        fCameraX = transform.position.x;
        for (int i = 0; i < 3; i++)
        {
            v3Positions[i] = goPositions[i].transform.position;
        }

        transform.DOPath(v3Positions, 10, PathType.CatmullRom, PathMode.Sidescroller2D).OnWaypointChange(_ => ChangeSite());

        // _fDefaultCameraViewField = Camera.main.fieldOfView;
        // Vector3 forward = transform.position + Vector3.back * 40;
        //
        // _sequence = DOTween.Sequence();
        //
        // var doJump = transform.DOJump(forward, 1, 10, 10).Pause();
        // _sequence.SetAutoKill(true);
        // var doRotate = transform.DORotate(transform.rotation.eulerAngles+Vector3.back, 5);
        // _sequence.Append(doJump).Join(doRotate).Pause();
        //
        // var tweenerZoom = DOTween.To(() =>
        //         Camera.main.orthographicSize,
        //     x => Camera.main.orthographicSize = x,
        //     4f, 0).Pause();
        //
        // var doRotateCamera = Camera.main.transform.DORotate(new Vector3(0, -200, 0), 1).Pause();
        // var doRotate2Right = Camera.main.transform.DORotate(new Vector3(0, -180, 0), 1).Pause();
        //
        // _doSequence = DOTween.Sequence();
        // _doSequence.Append(tweenerZoom).Append(doRotateCamera).Append(doRotate2Right).Pause();
    }

    void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                // DOTween.To(() => Camera.main.fieldOfView, x => Camera.main.fieldOfView = x, 30, 1);
                // _sequence.PlayForward();
                _doSequence.PlayForward();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
    }
}