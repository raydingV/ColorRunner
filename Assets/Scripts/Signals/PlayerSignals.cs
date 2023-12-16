using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public Func<float> onPlayerRotate;
        //change collectable material signals
        public Action<Material> onChangeMaterial;
        public Action<Color> onChangeColor;

        protected override void Awake()
        {
            base.Awake();
        }
    }
}