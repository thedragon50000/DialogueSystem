using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Serialization;

public class MusicScene : MonoBehaviour
{
    private IntReactiveProperty iIndex;
    private int _iLength;
    public GameObject Musicbase;

    [SerializeField] AudioClip _clip = null;
    public AudioSource[] audioSources;

    public TMP_Text textNowPlaying;

    // public string strmyClass.NowPlaying;
    public StringReactiveProperty strNowPlaying;

    public MyClass myClass;

    // public AudioSource NowPlaying;

    [Serializable]
    public class MyClass
    {
        public AudioSource NowPlaying;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        audioSources = Musicbase.GetComponentsInChildren<AudioSource>();
        myClass = new();
    }

    void Start()
    {
        myClass.ObserveEveryValueChanged(m => m.NowPlaying).Subscribe(_ =>
        {
            if (myClass.NowPlaying.IsUnityNull()) return;
            print("audio change.");
            strNowPlaying.Value = myClass.NowPlaying.name;
        });

        strNowPlaying.Subscribe(_ =>
        {
            textNowPlaying.text = strNowPlaying.Value;
        });
        
        myClass.NowPlaying = audioSources[0];
        _clip = myClass.NowPlaying.clip;
        myClass.NowPlaying.Play();
        iIndex.Subscribe(ChangeMusic, Error, OnCompleted);
        _iLength = audioSources.Length;

        var observableGetKeyUp = Observable.EveryUpdate().Where(_ => Input.GetKeyUp(KeyCode.RightArrow));
        observableGetKeyUp.Buffer(observableGetKeyUp.Throttle(TimeSpan.FromSeconds(.3f)).Where(s => s > 1)).Subscribe(
            _ =>
            {
                myClass.NowPlaying.Stop();
                print("Right.");
                iIndex.Value++;
                var i = iIndex.Value;

                myClass.NowPlaying = audioSources[i];
                _clip = audioSources[i].clip;

                myClass.NowPlaying.Play();
            });
        var observableGetKeyUp2 = Observable.EveryUpdate().Where(_ => Input.GetKeyUp(KeyCode.LeftArrow));
        observableGetKeyUp2.Buffer(observableGetKeyUp2.Throttle(TimeSpan.FromSeconds(.3f)).Where(s => s > 1)).Subscribe(
            _ =>
            {
                myClass.NowPlaying.Stop();
                print("Left.");
                audioSources[iIndex.Value].Stop();
                iIndex.Value--;
                var i = iIndex.Value;

                myClass.NowPlaying = audioSources[i];
                _clip = audioSources[i].clip;

                myClass.NowPlaying.Play();
            });
    }

    private void OnCompleted()
    {
        throw new NotImplementedException();
    }

    private void Error(Exception obj)
    {
        Debug.LogError(obj);
    }

    private void ChangeMusic(int obj)
    {
        var _index = obj;

        if (_index > _iLength - 1)
        {
            _index = 0;
        }

        else if (_index < 0)
        {
            _index = _iLength - 1;
        }

        iIndex.Value = _index;
    }

    // Update is called once per frame
    void Update()
    {
    }
}