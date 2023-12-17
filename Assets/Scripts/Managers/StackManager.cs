using Commands;
using DG.Tweening;
using Enums;
using Signals;
using System.Collections.Generic;
using UnityEngine;
using UnityObject;
using ValueObject;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables

        [SerializeField] private List<Transform> _collectable = new List<Transform>();
        [SerializeField] private List<Transform> _tempList = new List<Transform>();

        [SerializeField] private ColorType colorType;
        [SerializeField] private GameObject stickmanPrefab;

        #endregion Serialize Variables

        #region private Variables

        private AddCollectablesAfterDroneAnimationDoneCommand _addCollectablesAfterDroneAnimationDoneCommand;
        private AddStackCommand _addStackCommand;
        private ChangeAllCollectableColorTypeCommand _changeAllCollectableColor;
        private GetFirstCollectableCommand _getFirstCollectableCommand;
        private InitializeStackOnStartCommand _initializeStackOnStartCommand;
        private LerpData _lerpData;
        private Transform _playerPossition;
        private RemoveStackCommand _removeStackCommand;
        private ShakeStackCommand _shakeStakeCommand;
        private StackEnterDroneAreaCommand _stackEnterDroneAreaCommand;
        private StackLerpMoveCommand _stackLerpMoveCommand;
        #endregion private Variables

        #endregion Self Variables

        private void Awake()
        {
            Initialize();
            _initializeStackOnStartCommand.OnInitializeStackOnStart(6);//test pupose that bind next level signal
        }

        private void FixedUpdate()
        {
            _stackLerpMoveCommand.OnLerpStackMove();
        }

        private LerpData GetLerpData() => Resources.Load<CD_Lerp>("Data/CD_Lerp").Data;

        #region Event Subscriptions

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            StackSignals.Instance.onAddStack += _addStackCommand.OnAddStack;
            StackSignals.Instance.onRemoveFromStack += _removeStackCommand.OnRemoveFromStack;
            StackSignals.Instance.onLerpStack += _stackLerpMoveCommand.OnLerpStackMove;
            StackSignals.Instance.onSetStackStartSize += _initializeStackOnStartCommand.OnInitializeStackOnStart;
            //StackSignals.Instance.onThrowStackInMiniGame += OnThrowStackInMiniGame;
            StackSignals.Instance.onStackEnterDroneArea += _stackEnterDroneAreaCommand.OnStackEnterDroneArea;
            StackSignals.Instance.onMergeToPLayer += OnMergeToPLayer;
            StackSignals.Instance.onAddAfterDroneAnimationDone += _addCollectablesAfterDroneAnimationDoneCommand.OnAddCollectablesAfterDroneAnimationDone;
            StackSignals.Instance.onGetFirstCollectable += _getFirstCollectableCommand.OnGetFirstCollectable;
            PlayerSignals.Instance.onChangeAllCollectableColorType += _changeAllCollectableColor.OnChangeAllCollectableColorType;
        }

        private void UnSubscribe()
        {
            StackSignals.Instance.onAddStack -= _addStackCommand.OnAddStack;
            StackSignals.Instance.onRemoveFromStack -= _removeStackCommand.OnRemoveFromStack;
            StackSignals.Instance.onLerpStack -= _stackLerpMoveCommand.OnLerpStackMove;
            StackSignals.Instance.onSetStackStartSize -= _initializeStackOnStartCommand.OnInitializeStackOnStart;
            //StackSignals.Instance.onThrowStackInMiniGame -= OnThrowStackInMiniGame;
            StackSignals.Instance.onStackEnterDroneArea -= _stackEnterDroneAreaCommand.OnStackEnterDroneArea;
            StackSignals.Instance.onMergeToPLayer -= OnMergeToPLayer;
            StackSignals.Instance.onAddAfterDroneAnimationDone -= _addCollectablesAfterDroneAnimationDoneCommand.OnAddCollectablesAfterDroneAnimationDone;
            StackSignals.Instance.onGetFirstCollectable += _getFirstCollectableCommand.OnGetFirstCollectable;
            PlayerSignals.Instance.onChangeAllCollectableColorType -= _changeAllCollectableColor.OnChangeAllCollectableColorType;
        }

        #endregion Event Subscriptions

        private void Initialize()
        {
            _playerPossition = GameObject.FindGameObjectWithTag("Player").transform;
            _lerpData = GetLerpData();
            _shakeStakeCommand = new ShakeStackCommand(ref _collectable, ref _lerpData);
            _addStackCommand = new AddStackCommand(ref _collectable, ref _shakeStakeCommand, transform, this);
            _removeStackCommand = new RemoveStackCommand(ref _collectable);
            _stackLerpMoveCommand = new StackLerpMoveCommand(ref _collectable, ref _lerpData, _playerPossition);
            _stackEnterDroneAreaCommand = new StackEnterDroneAreaCommand(ref _collectable, ref _tempList);
            _addCollectablesAfterDroneAnimationDoneCommand = new AddCollectablesAfterDroneAnimationDoneCommand(ref _collectable, ref _tempList);
            _changeAllCollectableColor = new ChangeAllCollectableColorTypeCommand();
            _initializeStackOnStartCommand = new InitializeStackOnStartCommand(ref _collectable, _playerPossition, transform, stickmanPrefab, colorType);
            _getFirstCollectableCommand = new GetFirstCollectableCommand(ref _collectable);
        }

        private void OnMergeToPLayer()
        {
            _tempList = _collectable;
            _collectable.Clear();
            _tempList.TrimExcess();
            _collectable.TrimExcess();

            foreach (var stack in _tempList)
            {
                stack.DOMoveZ(_playerPossition.position.z, 0.4f).OnComplete(() =>
                    _playerPossition.DOScale(
                        new Vector3(_playerPossition.localScale.x + 0.0375f, _playerPossition.localScale.y + 0.0375f,
                            _playerPossition.localScale.z + 0.0375f), 0.3f));
            }
        }
    }
}