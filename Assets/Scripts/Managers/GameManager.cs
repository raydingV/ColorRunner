using System;
using Enums;
using Extentions;
using Keys;
using Signals;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public GameStates States;

    #endregion

    #endregion

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    
    private void OnEnable()
    {
        SubscribeEvents();
    }


    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
        CoreGameSignals.Instance.onSaveGameData += OnSaveGame;
        CoreGameSignals.Instance.onReset += OnReset;
        
        PlayerSignals.Instance.onPlayerEnterIdleArea += OnPlayerEnterIdleArea;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
        CoreGameSignals.Instance.onSaveGameData -= OnSaveGame;
        CoreGameSignals.Instance.onReset -= OnReset;
        
        PlayerSignals.Instance.onPlayerEnterIdleArea -= OnPlayerEnterIdleArea;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void OnChangeGameState(GameStates newState)
    {
        States = newState;
    }

    private void OnPlayerEnterIdleArea() => OnChangeGameState(GameStates.Idle);

    
    
    
    private void OnSaveGame(SaveGameDataParams saveDataParams)
    {
        if (saveDataParams.Level != null)
        {
            ES3.Save("Level", saveDataParams.Level);
        }
        /*
        if (saveDataParams.Coin != null) ES3.Save("Coin", saveDataParams.Coin);
        if (saveDataParams.SFX != null) ES3.Save("SFX", saveDataParams.SFX);
        if (saveDataParams.VFX != null) ES3.Save("VFX", saveDataParams.VFX);
        if (saveDataParams.Haptic != null) ES3.Save("Haptic", saveDataParams.Haptic);
        */
    }


    private void OnReset()
    {
        OnChangeGameState(GameStates.Runner);
    }
    
}