﻿using System;
using Extentions;
using Keys;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction onRunnerSaveData = delegate { };
        public UnityAction onIdleSaveData = delegate {  };
        public Func<SaveRunnerGameDataParams> onRunnerGameLoad;
        
        public Func<int> onGetRunnerLevelID = delegate { return 0; };
        public Func<int> onGetIdleLevelId = delegate { return 0;};

        public Action onDataGet = delegate {  };
        public UnityAction<SaveIdleGameDataParams> onSaveIdleParams = delegate { };
        public Func<SaveIdleGameDataParams> onLoadIdleGame;
    }
}