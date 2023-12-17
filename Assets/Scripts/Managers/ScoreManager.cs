using System;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Variables

        private int currentScore;
        private int totalScore;

        #endregion

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onCurrentLevelScoreUpdate += OnCurrentLevelScoreUpdate;
            ScoreSignals.Instance.onTotalScoreUpdate += OnTotalScoreUpdate;
            
            ScoreSignals.Instance.currentScore += ReturnCurrentScore;
            ScoreSignals.Instance.totalScore += ReturnTotalScore;
        }
        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onCurrentLevelScoreUpdate -= OnCurrentLevelScoreUpdate;
            ScoreSignals.Instance.onTotalScoreUpdate -= OnTotalScoreUpdate;
            
            ScoreSignals.Instance.currentScore -= ReturnCurrentScore;
            ScoreSignals.Instance.totalScore -= ReturnTotalScore;
        }

        #endregion
        
        private void OnCurrentLevelScoreUpdate(int score)
        {
            currentScore += score;
            UpdateScoreParams();
        }

        private void OnTotalScoreUpdate(int score)
        {
            totalScore += score;
            UpdateScoreParams();
        }

        private void UpdateScoreParams()
        {
            new ScoreParams() {currentLevelScore = currentScore, totalScore = totalScore};
        }
        
        private int ReturnCurrentScore() {return currentScore;}
        
        private int ReturnTotalScore() {return totalScore;}
    }
}