using Enums;
using Extentions;
using Keys;
using System;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<GameStates> onChangeGameState = delegate { };
        public UnityAction<SaveGameDataParams> onSaveGameData = delegate { };
        public UnityAction onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };

        public UnityAction onLevelFailed = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };

        public Func<int> onGetLevelID = delegate { return 0; };
    }
}