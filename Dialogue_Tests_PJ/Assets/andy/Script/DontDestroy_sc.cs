using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using UniRx;
using UnityEngine.Audio;
using DG.Tweening;

public class DontDestroy_sc : MonoBehaviour
{
    public AudioSource audio;
    public AudioMixerGroup mixer1;
    public AudioMixerGroup mixer15;
    public AudioMixerGroup mixer2;

    private void Start()
    {
        Observable.EveryUpdate().ThrottleFirst(TimeSpan.FromSeconds(1)).Subscribe(_ => PlaySound());

        IEnumerator i = PlayFourTimes_ChangePitch();
        var fromCoroutine = Observable.FromCoroutine(PlayFourTimes_ChangePitch);
        Observable.EveryUpdate().ThrottleFirst(TimeSpan.FromSeconds(12))
            .Subscribe(_ => { fromCoroutine.StartAsCoroutine(); });
    }

    public IEnumerator PlayFourTimes_ChangePitch()
    {
        yield return audio.outputAudioMixerGroup = mixer1;
        yield return new WaitForSeconds(4);
        yield return audio.outputAudioMixerGroup = mixer15;
        yield return new WaitForSeconds(4);
        yield return audio.outputAudioMixerGroup = mixer2;
        yield return new WaitForSeconds(4);
    }

    private void PlaySound()
    {
        audio.Play();
    }
}