﻿using System;
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
        public Action onDroneAnimationComplated;
        public Action<Transform> onWrongTurretMatAreaEntered;
        public Action<OutlineType> onActivateOutlineTrasition;
        public Action onMergeToPLayer;
        public Action<bool, Transform> onAddAfterDroneAnimationDone;
    }
}