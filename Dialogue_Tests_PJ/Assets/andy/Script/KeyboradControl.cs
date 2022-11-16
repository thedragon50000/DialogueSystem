using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;
using Zenject;

public class KeyboradControl : MonoBehaviour
{
    [Inject] private AudioManager audio;
    
    [Inject]
    private Without_MonoBehavier _withoutMono;
    
    [Inject]
    void Init()
    {
        print("KeyBoard Initialized");
    }
    
    private void Start()
    {
        print("_uniRxSc.i = "+_withoutMono.i);
        _withoutMono.Show();
        
    }

    void Update()
    {
        //todo: 用Enum來代替字串
        if (Input.GetKeyDown(KeyCode.Q))
        {
            audio.PlayBGM("Lobby_Main");
            
            audio.PlayBGM(E_BGM.Lobby_Main.ToString());

            E_BGM temp = E_BGM.Lobby_Main;
            audio.PlayBGM(temp.ToString());
        }

        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     AudioManager.inst.PlayBGM("Game_DT");
        // }
        //
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     AudioManager.inst.PlaySFX("Btn_01");
        // }
        //
        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     AudioManager.inst.PlaySFX("Btn_02");
        // }
        //
        // if (Input.GetKeyDown(KeyCode.Z))
        // {
        //     AudioManager.inst.BGMStop();
        // }
        //
        // if (Input.GetKeyDown(KeyCode.X))
        // {
        //     AudioManager.inst.SFXStop();
        // }
        //
        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     AudioManager.inst.BGMReset(1);
        // }
        //
        // if (Input.GetKeyDown(KeyCode.V))
        // {
        //     AudioManager.inst.SFXReset(1);
        // }
    }
}

public enum E_BGM
{
    Lobby_Main,
    Game_DT,
}