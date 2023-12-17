using System;
using System.Threading.Tasks;
using Commands;
using Controllers;
using DG.Tweening;
using Enums;
using Signals;
using StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelController uiPanelController;
        [SerializeField] private int increaceStackSize;
        [SerializeField] private Image arrow;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI prizeText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private int arrowMoveAmount;
        [SerializeField] private float arrowMoveDuration;
        #endregion

        #endregion

        #region Private Variables

        private int score, prizeScore, scoreMultiplier; // score = Scoresignalsden cekilecek.
        private bool isPrize;
        private PrizeArrowMoveCommand _prizeArrowMoveCommand;

        #endregion

        private void Awake()
        {
            _prizeArrowMoveCommand = new PrizeArrowMoveCommand(ref arrow, ref arrowMoveAmount, ref arrowMoveDuration);
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            isPrize = true;
            SubscribeEvents();
            IdleMoneyMultiplier();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetLevelText += OnSetLevelText;
            UISignals.Instance.onIdleMoneyMultiplier += OnIdleMoneyMultiplier;
            
            ScoreSignals.Instance.onUpdateScoreText += UpdateScoreText;

            CoreGameSignals.Instance.onPlay += OnPlay;
            
            PlayerSignals.Instance.onPlayerExitDroneArea += OnPlayerExitDroneArea;
            
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetLevelText -= OnSetLevelText;
            UISignals.Instance.onIdleMoneyMultiplier -= OnIdleMoneyMultiplier;
            
            ScoreSignals.Instance.onUpdateScoreText -= UpdateScoreText;
            
            CoreGameSignals.Instance.onPlay -= OnPlay;
            
            PlayerSignals.Instance.onPlayerExitDroneArea -= OnPlayerExitDroneArea;
            
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenPanel(panelParam);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.ClosePanel(panelParam);
        }
        private void OnSetLevelText(int value)
        {
            levelText.text = "Level " + value;
        }

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.PreGamePanel);
            PlayerSignals.Instance.onTranslateCollectableAnimationState(new RunnerAnimationState());
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Runner);
        }

        private void OnLevelFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LoseGamePanel);
        }

        private void OnLevelSuccessful()
        {
            isPrize = true;
            score = ScoreSignals.Instance.currentScore();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.EndGamePrizePanel);
            IdleMoneyMultiplier();
        }

        private async void OnPlayerExitDroneArea()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.X2Panel);
            await Task.Delay(500);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.X2Panel);
        }
        
        public void Play()
        {
            PlayerSignals.Instance.onTranslateCollectableAnimationState(new RunnerAnimationState());
            CoreGameSignals.Instance.onPlay?.Invoke();
            scoreText.text = ScoreSignals.Instance.totalScore().ToString();
        }

        public void NextLevel()
        {
            isPrize = false;
            LevelSignals.Instance.onNextLevel?.Invoke();
            SaveSignals.Instance.onIdleSaveData?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.IdlePanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PreGamePanel);
            
            //Score Savele
        }

        public void RestartLevel()
        {
            LevelSignals.Instance.onRestartLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LoseGamePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.InGamePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PreGamePanel);
            OnReset();
        }

        public void AddStackButton()
        {
            StackSignals.Instance.onSetStackStartSize?.Invoke(increaceStackSize);
        }

        private void OnIdleMoneyMultiplier(int multiplier)
        {
            scoreMultiplier = multiplier;
            prizeScore = score * scoreMultiplier;
            prizeText.text = prizeScore.ToString();
        }
        
        public void ToggleHaptic()
        {
            //AddVibration
        }

        private void OnReset()
        {
            isPrize = false;
            prizeScore = 0;
            score = 0;
        }
        
        public void IdleMoneyMultiplier()
        {
            _prizeArrowMoveCommand.Execute();
        }
        
        public void ClaimButton()
        {
            ScoreSignals.Instance.onTotalScoreUpdate?.Invoke(prizeScore);
            ScoreSignals.Instance.onShowScoreIdle?.Invoke();
            UpdateScoreText();
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Idle);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.EndGamePrizePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.IdlePanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
            PlayerSignals.Instance.onTranslateCameraState?.Invoke(new CameraIdleState());
        }

        public void NoThanksButton()
        {
            prizeScore = score;
            ScoreSignals.Instance.onTotalScoreUpdate?.Invoke(prizeScore);
            ScoreSignals.Instance.onShowScoreIdle?.Invoke();
            UpdateScoreText();
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Idle);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.EndGamePrizePanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.IdlePanel);
            PlayerSignals.Instance.onTranslateCameraState?.Invoke(new CameraIdleState());
        }

        private void UpdateScoreText()
        {
            scoreText.text = ScoreSignals.Instance.totalScore().ToString();
        }
    }
}