using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class rotate_sc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(0, 120, 0), 2).SetLoops(-1, LoopType.Incremental);
    }

    // Update is called once per frame
    void Update()
    {
    }
}