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

        public Action onPlayerEnterIdleArea;

        public Action onPlayerScaleUp;
        public Action<Material> onChangeMaterial;
        public Action<AnimationStateMachine> onTranslateCollectableAnimationState;
        public Action<CameraStateMachine> onTranslateCameraState;
        public Action<ColorType> onChangeAllCollectableColorType;
        public Action<AnimationStateMachine> onTranslatePlayerAnimationState;
        public Func<Transform> onGetPlayerTransfrom = delegate { return null;};
        public Action onActivateObject;
        public Action onScaleDown;
        public Action onThrowParticule;
    }
}