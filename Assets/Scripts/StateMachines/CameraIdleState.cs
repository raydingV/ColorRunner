using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    public class CameraIdleState : CameraStateMachine
    {
        public override void ChangerStateCamera()
        {
            _runnerCamera.Follow = _target;
            _cinamationAnimationStates.Play("IdleCam");
        }
    }
}
