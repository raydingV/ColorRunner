using System;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<bool> onCurrentLevelScoreUpdate;
        public UnityAction<int> onTotalScoreUpdate;
        public Action onHideScore;
        public Action onShowScoreIdle;
        public Action onUpdateScoreAfterDroneArea;
        public Action onUpdateScoreText;

        public Func<int> currentScore;
        public Func<int> totalScore;
    }
}