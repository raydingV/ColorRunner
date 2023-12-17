﻿using Enums;
using Signals;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managers;
using StateMachine;
using Unity.Mathematics;
using UnityEngine;

namespace Commands
{
    public class InitializeStackOnStartCommand
    {
        private List<Transform> _collectable;
        private Transform _parent;
        private GameObject stickmanPrefab;
        private ColorType _colorType;

        public InitializeStackOnStartCommand(ref List<Transform> collectable, Transform parent, GameObject stickmanPrefab, ColorType colorType)
        {
            _collectable = collectable;
            _parent = parent;
            this.stickmanPrefab = stickmanPrefab;
            _colorType = colorType;
        }

        public void OnInitializeStackOnStart(int size)
        {
            if (_collectable.Count == 0)
            {
                GameObject firstInitialStack = GameObject.Instantiate(stickmanPrefab);
                firstInitialStack.transform.localPosition = new Vector3(0, .4f, 0);
                _collectable.Add(firstInitialStack.transform);
                firstInitialStack.transform.SetParent(_parent);
                ScoreSignals.Instance.onCurrentLevelScoreUpdate?.Invoke(true);
            }

            for (int i = 0; i < size; i++)
            {
                Vector3 frontStickmanPosition = _collectable[_collectable.Count - 1].transform.position;
                frontStickmanPosition = new Vector3(0, frontStickmanPosition.y,frontStickmanPosition.z - 1.5f);
                GameObject stackInstance = GameObject.Instantiate(stickmanPrefab, frontStickmanPosition,quaternion.identity);
                stackInstance.transform.SetParent(_parent);
                _collectable.Add(stackInstance.transform);
                ScoreSignals.Instance.onCurrentLevelScoreUpdate?.Invoke(true);
            }

            StackSignals.Instance.onSetScoreControllerPosition?.Invoke(_collectable[0]);
            _collectable.TrimExcess();

            PlayerSignals.Instance.onChangeAllCollectableColorType?.Invoke(StackSignals.Instance.onGetColorType());

            ScoreSignals.Instance.onHideScore?.Invoke();
        }
    }
}