using System;
using Extentions;
using UnityEngine;
using StateMachine;
using Enums;
using UnityEngine.Events;
using UnityEngine.WSA;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public Func<float> onPlayerRotate;
        public Action onPlayerEnterDroneArea;
        public Action onPlayerExitDroneArea;
        public Action onPlayerEnterTurretArea;
        public Action onPlayerExitTurretArea;
        public Action<Material> onChangeMaterial;
        public Action<AnimationStateMachine> onTranslateAnimationState;
        public Action<CameraStateMachine> onTranslateCameraState;
        public Action<ColorType> onChangeAllCollectableColorType;
    }
}