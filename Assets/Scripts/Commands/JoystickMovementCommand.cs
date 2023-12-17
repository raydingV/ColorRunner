using Signals;
using Keys;

namespace Commands
{
    public class JoystickMovementCommand
    {
        private FloatingJoystick _floatingJoystick;

        public JoystickMovementCommand(ref FloatingJoystick floatingJoystick)
        {
            _floatingJoystick = floatingJoystick;
        }
        
        public void JoystickMovement()
        {
            float horizontal = _floatingJoystick.Horizontal;
            float vertical = _floatingJoystick.Vertical;
            
            InputSignals.Instance.onInputParamsUpdate?.Invoke(new InputParams() {
                XValue = horizontal, YValue = vertical });
        }
    }
}