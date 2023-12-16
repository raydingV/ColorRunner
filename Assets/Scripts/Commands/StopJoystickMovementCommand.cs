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
            InputSignals.Instance.onInputParamsUpdate?.Invoke(new InputParameters()
            {
                ValueOfX = 0, ValueOfY = 0
            });
        }
    }
}