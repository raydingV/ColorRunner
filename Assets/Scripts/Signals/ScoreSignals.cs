using System;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction onCurrentLevelScoreUpdate;
        public UnityAction<int> onTotalScoreUpdate;
        public Action onHideScore;
        public Action onUpdateScoreAfterDroneArea;

        public Func<int> currentScore;
        public Func<int> totalScore;
    }
}