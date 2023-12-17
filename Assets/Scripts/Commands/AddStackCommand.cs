using System;
using System.Collections.Generic;
using System.Linq;
using Signals;
using UnityEngine;

namespace Commands
{
    public class AddStackCommand
    {

        private List<Transform> _collectable;
        private ShakeStackCommand _command;
        private Transform _transform;
        private MonoBehaviour _monoBehaviour;

        public AddStackCommand(ref List<Transform> collectable,ref ShakeStackCommand command, Transform transform, MonoBehaviour monoBehaviour)
        {
            _collectable = collectable;
            _command = command;
            _transform = transform;
            _monoBehaviour = monoBehaviour;
        }

        public void OnAddStack(Transform collectable)
        {
            collectable.tag = "Collected";
            collectable.SetParent(_transform);
            _collectable.Add(collectable);
            _collectable.TrimExcess();
            _monoBehaviour.StartCoroutine(_command.ShakeStackSize());
            StackSignals.Instance.onSetScoreControllerPosition?.Invoke(_collectable[0]);
            ScoreSignals.Instance.onCurrentLevelScoreUpdate?.Invoke();
        }
    }
}