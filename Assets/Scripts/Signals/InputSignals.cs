using Enums;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction onEnableInput = delegate {  };
        public UnityAction onDisableInput = delegate {  };
        public UnityAction onFirstTimeTouchTaken = delegate { };
        public UnityAction onPointerDown = delegate { };
        public UnityAction onPointerDragged = delegate { };
        public UnityAction onPointerReleased = delegate { };
        public UnityAction<JoystickStates> onJoystickStateChange;
        public UnityAction<InputParams> onInputParamsUpdate = delegate {  };
    }
}