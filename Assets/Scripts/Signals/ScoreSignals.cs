using System;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<bool> onCurrentLevelScoreUpdate;
        public UnityAction<int> onTotalScoreUpdate;
        
        public Action onShowScore;
        public Action onHideScore;
        public Action onShowScoreIdle;
        public Action onUpdateScoreText;

        public Action onUpdateScoreAfterDroneArea;
        
        public Func<int> currentScore;
        public Func<int> totalScore;
    }
}