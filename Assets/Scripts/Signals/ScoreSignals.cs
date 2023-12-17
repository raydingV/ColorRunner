using System;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<int> onCurrentLevelScoreUpdate;
        public UnityAction<int> onTotalScoreUpdate;

        public Func<int> currentScore;
        public Func<int> totalScore;
    }
}