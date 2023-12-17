using Signals;
using Keys;

namespace Commands
{
    public class StopJoystickMovementCommand
    {
        public StopJoystickMovementCommand()
        {
            
        }
        
        public void StopJoystickMovement()
        {
            InputSignals.Instance.onInputParamsUpdate?.Invoke(new InputParams()
            {
                XValue = 0, YValue = 0
            });
        }
    }
}