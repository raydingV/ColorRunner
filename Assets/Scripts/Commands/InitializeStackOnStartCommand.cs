using Enums;
using Signals;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Commands
{
    public class InitializeStackOnStartCommand
    {
        private List<Transform> _collectable;
        private Transform _playerPossition;
        private Transform _parent;
        private GameObject stickmanPrefab;
        private ColorType _colorType;

        public InitializeStackOnStartCommand(ref List<Transform> collectable, Transform playerPossition, Transform parent, GameObject stickmanPrefab, ColorType colorType)
        {
            _collectable = collectable;
            _playerPossition = playerPossition;
            _parent = parent;
            this.stickmanPrefab = stickmanPrefab;
            _colorType = colorType;
        }

        public void OnInitializeStackOnStart(int size)
        {
            GameObject firstInitialStack = GameObject.Instantiate(stickmanPrefab, _playerPossition);
            _collectable.Add(firstInitialStack.transform);
            firstInitialStack.transform.SetParent(_parent);
            ScoreSignals.Instance.onCurrentLevelScoreUpdate?.Invoke();

            for (int i = 0; i < size; i++)
            {
                GameObject stackInstance = GameObject.Instantiate(stickmanPrefab, _collectable.Last());
                stackInstance.transform.SetParent(_parent);
                _collectable.Add(stackInstance.transform);
                ScoreSignals.Instance.onCurrentLevelScoreUpdate?.Invoke();
            }

            StackSignals.Instance.onSetScoreControllerPosition?.Invoke(_collectable[0]);
            _collectable.TrimExcess();

            PlayerSignals.Instance.onChangeAllCollectableColorType?.Invoke(_colorType);

            ScoreSignals.Instance.onHideScore?.Invoke();
        }
    }
}