using System;
using Extentions;

namespace Signals
{
    public class RunnerSignals : MonoSingleton<RunnerSignals>
    {
        public Action onDroneAnimationComplated;
    }
}