using System;
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

        private int score = 100, prizeScore, scoreMultiplier; // score = Scoresignalsden cekilecek.
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
            UISignals.Instance.onUpdateStageData += OnUpdateStageData;
            UISignals.Instance.onSetLevelText += OnSetLevelText;
            UISignals.Instance.onIdleMoneyMultiplier += OnIdleMoneyMultiplier;
            CoreGameSignals.Instance.onPlay += OnPlay;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onUpdateStageData -= OnUpdateStageData;
            UISignals.Instance.onSetLevelText -= OnSetLevelText;
            UISignals.Instance.onIdleMoneyMultiplier -= OnIdleMoneyMultiplier;
            CoreGameSignals.Instance.onPlay -= OnPlay;
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

        private void OnUpdateStageData(int value)
        {
            
        }

        private void OnSetLevelText(int value)
        {
            
        }

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.PreGamePanel);
            PlayerSignals.Instance.onTranslateAnimationState(new RunnerAnimationState());
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
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.EndGamePrizePanel);
            IdleMoneyMultiplier();
        }

        public void Play()
        {
            PlayerSignals.Instance.onTranslateAnimationState(new RunnerAnimationState());
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void NextLevel()
        {
            isPrize = false;
            LevelSignals.Instance.onNextLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.IdlePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PreGamePanel);
        }

        public void RestartLevel()
        {
            LevelSignals.Instance.onRestartLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PreGamePanel);
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
            //prizeScoreu Signalse gonder.
        }

        public void NoThanksButton()
        {
            prizeScore = score;
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.EndGamePrizePanel);
        }
    }
}