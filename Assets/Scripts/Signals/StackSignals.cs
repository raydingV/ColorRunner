using System;
using Enums;
using UnityEngine;
using Extentions;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public Action<Transform> onAddStack;
        public Action<Transform> onRemoveFromStack;
        public Action<int> onSetStackStartSize;
        public Action onLerpStack;
        public Action onShakeStackSize;
        public Action onThrowStackInMiniGame;
        public Action<Transform, Transform> onStackEnterDroneArea;
        public Action onLastCollectableEnterDroneArea;
        public Action<Transform> onSetScoreControllerPosition;
        public Action<Transform> onWrongTurretMatAreaEntered;
        public Action<OutlineType> onActivateOutlineTrasition;
        public Action onMergeToPLayer;
        public Action<ColorType> onChangeMatarialColor;
        public Func<Transform> onGetFirstCollectable;
        public Action<bool, Transform> onAddAfterDroneAnimationDone;
    }
}
