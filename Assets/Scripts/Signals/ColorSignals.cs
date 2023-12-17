using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Extentions;

namespace Signals
{
    public class ColorSignals : MonoSingleton<ColorSignals>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public Action onSetMarialRandomColor;
        public Action onSetMarialGradiensColor;
    }
}
