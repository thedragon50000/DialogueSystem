using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using Zenject;

public class NotebookButton_sc : MonoBehaviour
{
    public Transform notebook;
    public Transform turnPage;
    private int iButtonClickTimes = 0;
    public GameObject goNotebookWords;

    private void Awake()
    {
        // btn = this.gameObject.GetComponent<Button>();
    }

    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        var tweener2 = turnPage.DOLocalRotate(new Vector3(0, 0, 180), 1f);
        tweener2.Pause();
        var tweener = notebook.DOScale(new Vector3(33, 33, 33), 1f);
        tweener.Pause();
        
        btn.OnClickAsObservable().Subscribe(_ =>
        {
            var co = Observable.FromCoroutine(coOpenNotebook);
            co.StartAsCoroutine();
        });
    }

    IEnumerator coOpenNotebook()
    {
        if (iButtonClickTimes == 0)
        {
            notebook.gameObject.SetActive(true);
            turnPage.DOPlayForward();
            yield return new WaitForSeconds(1);
            notebook.DOPlayForward();
            yield return new WaitForSeconds(1);
            notebook.gameObject.SetActive(false);
            iButtonClickTimes++;
        }

        goNotebookWords.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }
}