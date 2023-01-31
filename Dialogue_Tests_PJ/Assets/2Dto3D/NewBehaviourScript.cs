using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform goose;
    // Start is called before the first frame update
    void Start()
    {
        Tweener tweener,tweener2;
        Sequence sequence = DOTween.Sequence();
        tweener = goose.DORotate(new Vector3(0, 0, 10), 2f).SetEase(Ease.InOutSine);
        tweener2 = goose.DORotate(new Vector3(0, 70, 0), 2f).SetEase(Ease.InOutBounce);
        tweener.Pause();
        tweener2.Pause();

        sequence.Append(tweener).Append(tweener2).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
