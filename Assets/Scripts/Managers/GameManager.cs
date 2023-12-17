using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables
    
        public GameStates States;
    
        #endregion Public Variables

        #region Serialized

        [SerializeField] private GameObject Fog;

        #endregion

        #region Private

        private bool isGameRunning;

        #endregion
        
        #endregion Self Variables
    
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    
        private void OnEnable()
        {
            SubscribeEvents();
            Fog.SetActive(true);
        }
    
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onGetGameState += OnGetGameState;
            CoreGameSignals.Instance.onIsGameRunning += IsGameRunning;
    
            PlayerSignals.Instance.onPlayerEnterIdleArea += OnPlayerEnterIdleArea;
        }
    
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onGetGameState -= OnGetGameState;
            CoreGameSignals.Instance.onIsGameRunning -= IsGameRunning;
            
            PlayerSignals.Instance.onPlayerEnterIdleArea -= OnPlayerEnterIdleArea;
        }
    
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnPlay()
        {
            ChangeGameRunningState(true);
        }
    
        private void OnChangeGameState(GameStates newState)
        {
            States = newState;
            ControlFog(newState);
        }
    
        private void ControlFog(GameStates newState) => Fog.SetActive(newState != GameStates.Idle);


        private void OnPlayerEnterIdleArea()
        {
            OnChangeGameState(GameStates.Idle);
            ChangeGameRunningState(false);
        }
        
        private GameStates OnGetGameState() => States;
        
        public bool IsGameRunning() => isGameRunning;

        private void ChangeGameRunningState(bool isRunning) => isGameRunning = isRunning;
        
        private void OnReset()
        {
            Fog.SetActive(true);
            OnChangeGameState(GameStates.Runner);
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Runner);
        }
    }
}