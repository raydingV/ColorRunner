using System;
using Enums;
using Extentions;
using Keys;
using Signals;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameStates States;

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
        
    }

    private void UnsubscribeEvents()
    {
        
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void OnChangeGameState(GameStates newState)
    {
        States = newState;
    }

}