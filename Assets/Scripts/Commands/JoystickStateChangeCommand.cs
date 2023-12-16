using Enums;
using UnityEngine.UI;

namespace Commands
{
    public class JoystickStateChangeCommand
    {
        private FloatingJoystick _floatingJoystick;
        private Image _joystickHandleImg;
        private Image _joystickBackgroundImg;
        

        public JoystickStateChangeCommand(ref FloatingJoystick floatingJoystick, ref Image joystickHandleImg, ref Image joystickBackgroundImg)
        {
            _floatingJoystick = floatingJoystick;
            _joystickHandleImg = joystickHandleImg;
            _joystickBackgroundImg = joystickBackgroundImg;
        }
        
        public void OnJoystickStateChange(JoystickStates joystickState)
        {
            switch (joystickState)
            {
                case JoystickStates.Runner:
                    _floatingJoystick.AxisOptions = global::AxisOptions.Horizontal;
                    _joystickHandleImg.enabled = false;
                    _joystickBackgroundImg.enabled = false;
                    break;
                
                case JoystickStates.Idle:
                    _floatingJoystick.AxisOptions = global::AxisOptions.Both;
                    _joystickHandleImg.enabled = true;
                    _joystickBackgroundImg.enabled = true;
                    break;
            }
        }
    }
}