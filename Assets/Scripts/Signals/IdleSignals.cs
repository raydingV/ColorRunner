using System;
using Extentions;
using UnityEngine;

namespace Signals
{
    public class IdleSignals : MonoSingleton<IdleSignals>
    {
        public Action<string,string> onPlayerEnterBuildingArea;
    }
}