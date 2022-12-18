using System;
using System.Collections;
using System.Collections.Generic;
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
    public AudioSource NowPlaying;

    // Start is called before the first frame update
    private void Awake()
    {
        audioSources = Musicbase.GetComponentsInChildren<AudioSource>();
    }

    void Start()
    {
        NowPlaying = audioSources[0];
        _clip = NowPlaying.clip;
        NowPlaying.Play();
        iIndex.Subscribe(ChangeMusic, Error, OnCompleted);
        _iLength = audioSources.Length;

        var observableGetKeyUp = Observable.EveryUpdate().Where(_ => Input.GetKeyUp(KeyCode.RightArrow));
        observableGetKeyUp.Buffer(observableGetKeyUp.Throttle(TimeSpan.FromSeconds(.3f)).Where(s => s > 1)).Subscribe(
            _ =>
            {
                NowPlaying.Stop();
                print("Right.");
                iIndex.Value++;
                var i = iIndex.Value;
                
                NowPlaying = audioSources[i];
                _clip = audioSources[i].clip;
                
                NowPlaying.Play();
            });
        var observableGetKeyUp2 = Observable.EveryUpdate().Where(_ => Input.GetKeyUp(KeyCode.LeftArrow));
        observableGetKeyUp2.Buffer(observableGetKeyUp2.Throttle(TimeSpan.FromSeconds(.3f)).Where(s => s > 1)).Subscribe(
            _ =>
            {
                NowPlaying.Stop();
                print("Left.");
                audioSources[iIndex.Value].Stop();
                iIndex.Value--;
                var i = iIndex.Value;

                NowPlaying = audioSources[i];
                _clip = audioSources[i].clip;
                
                NowPlaying.Play();
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