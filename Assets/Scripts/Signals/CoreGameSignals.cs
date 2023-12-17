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
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
    }
}