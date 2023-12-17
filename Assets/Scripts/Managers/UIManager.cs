using System;
using Controllers;
using DG.Tweening;
using Enums;
using Signals;
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
        #endregion

        #endregion

        #region Private Variables

        private int score = 100, prizeScore, scoreMultiplier; // score = Scoresignalsden cekilecek.
        private bool isPrize;

        #endregion
        
        
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
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onUpdateStageData -= OnUpdateStageData;
            UISignals.Instance.onSetLevelText -= OnSetLevelText;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
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
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void NextLevel()
        {
            isPrize = false;
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.IdlePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PreGamePanel);
        }

        public void RestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PreGamePanel);
        }

        public void AddStackButton()
        {
            StackSignals.Instance.onSetStackStartSize?.Invoke(increaceStackSize);
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
        
        private void Update()   //Commande bol
        {
            if (isPrize)
            {
                if (arrow.rectTransform.position.x > 140 && arrow.rectTransform.position.x < 280)
                {
                    prizeText.text = (score * 2).ToString();
                    scoreMultiplier = 2;
                }
                else if (arrow.rectTransform.position.x >= 280 && arrow.rectTransform.position.x < 420)
                {
                    prizeText.text = (score * 3).ToString();
                    scoreMultiplier = 3;
                }
                else if (arrow.rectTransform.position.x >= 420 && arrow.rectTransform.position.x < 560)
                {
                    prizeText.text = (score * 5).ToString();
                    scoreMultiplier = 5;
                }
                else if (arrow.rectTransform.position.x >= 560 && arrow.rectTransform.position.x < 700)
                {
                    prizeText.text = (score * 3).ToString();
                    scoreMultiplier = 3;
                }
                else if (arrow.rectTransform.position.x >= 700)
                {
                    prizeText.text = (score * 2).ToString();
                    scoreMultiplier = 2;
                }
            }
        }

        public void IdleMoneyMultiplier()
        {
            arrow.transform.DOLocalMoveX(1300, 1f).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
        }

        public void ClaimButton()
        {
            prizeScore = score * scoreMultiplier;
            //Score Signalse gonder.
        }

        public void NoThanksButton()
        {
            prizeScore = score;
        }
    }
}