using System;
using Commands;
using DG.Tweening;
using Enums;
using Signals;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using StateMachine;
using UnityEngine;
using UnityObject;
using ValueObject;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables

        [SerializeField] private List<Transform> _collectableList = new List<Transform>();
        [SerializeField] private List<Transform> _tempList = new List<Transform>();

        [SerializeField] private ColorType colorType;
        [SerializeField] private GameObject stickmanPrefab;
        #endregion Serialize Variables

        #region private Variables

        private LerpData _lerpData;
        private Transform _playerPosition;
        
        private AddCollectablesAfterDroneAnimationDoneCommand _addCollectablesAfterDroneAnimationDoneCommand;
        private AddStackCommand _addStackCommand;
        private ChangeAllCollectableColorTypeCommand _changeAllCollectableColor;
        private GetFirstCollectableCommand _getFirstCollectableCommand;
        private InitializeStackOnStartCommand _initializeStackOnStartCommand;
        private RemoveStackCommand _removeStackCommand;
        private ShakeStackCommand _shakeStakeCommand;
        private StackEnterDroneAreaCommand _stackEnterDroneAreaCommand;
        private StackLerpMoveCommand _stackLerpMoveCommand;

        #endregion private Variables

        #endregion Self Variables

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            _initializeStackOnStartCommand.OnInitializeStackOnStart(5);
        }

        private void FixedUpdate()
        {
            if(_playerPosition == null) return;
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
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            
            StackSignals.Instance.onAddStack += _addStackCommand.OnAddStack;
            StackSignals.Instance.onRemoveFromStack += _removeStackCommand.OnRemoveFromStack;
            StackSignals.Instance.onCollectableRemovedFromStack += OnCollectableRemovedFromStack;
            StackSignals.Instance.onSetStackStartSize += _initializeStackOnStartCommand.OnInitializeStackOnStart;
            StackSignals.Instance.onStackEnterDroneArea += _stackEnterDroneAreaCommand.OnStackEnterDroneArea;
            StackSignals.Instance.onMergeToPLayer += OnMergeToPLayer;
            StackSignals.Instance.onAddAfterDroneAnimationDone += _addCollectablesAfterDroneAnimationDoneCommand.OnAddCollectablesAfterDroneAnimationDone;
            StackSignals.Instance.onGetFirstCollectable += _getFirstCollectableCommand.OnGetFirstCollectable;
            PlayerSignals.Instance.onChangeAllCollectableColorType += _changeAllCollectableColor.OnChangeAllCollectableColorType;
            PlayerSignals.Instance.onChangeAllCollectableColorType += OnChangeColorType;
            StackSignals.Instance.onGetColorType += GetColorType;
            PlayerSignals.Instance.onPlayerExitDroneArea += OnCollectableRemovedFromStack;
        }

        private void UnSubscribe()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            
            StackSignals.Instance.onAddStack -= _addStackCommand.OnAddStack;
            StackSignals.Instance.onRemoveFromStack -= _removeStackCommand.OnRemoveFromStack;
            StackSignals.Instance.onCollectableRemovedFromStack -= OnCollectableRemovedFromStack;
            StackSignals.Instance.onSetStackStartSize -= _initializeStackOnStartCommand.OnInitializeStackOnStart;
            StackSignals.Instance.onStackEnterDroneArea -= _stackEnterDroneAreaCommand.OnStackEnterDroneArea;
            StackSignals.Instance.onMergeToPLayer -= OnMergeToPLayer;
            StackSignals.Instance.onAddAfterDroneAnimationDone -= _addCollectablesAfterDroneAnimationDoneCommand.OnAddCollectablesAfterDroneAnimationDone;
            StackSignals.Instance.onGetFirstCollectable -= _getFirstCollectableCommand.OnGetFirstCollectable;
            PlayerSignals.Instance.onChangeAllCollectableColorType -= _changeAllCollectableColor.OnChangeAllCollectableColorType;
            PlayerSignals.Instance.onChangeAllCollectableColorType -= OnChangeColorType;
            StackSignals.Instance.onGetColorType -= GetColorType;
            PlayerSignals.Instance.onPlayerExitDroneArea -= OnCollectableRemovedFromStack;
        }

        #endregion Event Subscriptions

        private void Initialize()
        {
            _lerpData = GetLerpData();
            _shakeStakeCommand = new ShakeStackCommand(ref _collectableList, ref _lerpData);
            _addStackCommand = new AddStackCommand(ref _collectableList, ref _shakeStakeCommand, transform, this);
            _removeStackCommand = new RemoveStackCommand(ref _collectableList);
            _stackEnterDroneAreaCommand = new StackEnterDroneAreaCommand(ref _collectableList, ref _tempList);
            _addCollectablesAfterDroneAnimationDoneCommand = new AddCollectablesAfterDroneAnimationDoneCommand(ref _collectableList, ref _tempList);
            _changeAllCollectableColor = new ChangeAllCollectableColorTypeCommand();
            _initializeStackOnStartCommand = new InitializeStackOnStartCommand(ref _collectableList, transform, stickmanPrefab, colorType);
            _getFirstCollectableCommand = new GetFirstCollectableCommand(ref _collectableList);
        }

        private void OnPlay()
        {
            _playerPosition = PlayerSignals.Instance.onGetPlayerTransfrom();
            _stackLerpMoveCommand = new StackLerpMoveCommand(ref _collectableList, ref _lerpData, _playerPosition);
        }

        private async void OnMergeToPLayer()
        {
            for (int i = 0; i < _collectableList.Count; i++)
            {
                Transform collectable = _collectableList[i];
                
                PlayerSignals.Instance.onPlayerScaleUp?.Invoke();
                
                collectable.DOMoveZ(_playerPosition.position.z, 0.1f).OnComplete(() =>
                {
                    collectable.gameObject.SetActive(false);
                    _tempList.Add(collectable);
                });
                await Task.Delay(100);
            }
            await Task.Delay(100);
            
            _collectableList.Clear();
            _collectableList.TrimExcess();
            PlayerSignals.Instance.onTranslateCameraState?.Invoke(new CameraMiniGameState());
            LevelSignals.Instance.onLevelSuccessful?.Invoke();
        }

        private int GetCollectableCount() => _collectableList.Count + _tempList.Count;
        
        private void OnCollectableRemovedFromStack()
        {
            if (GetCollectableCount() == 0)
            {
                print("Level Failed");
                LevelSignals.Instance.onLevelFailed?.Invoke();
                ScoreSignals.Instance.onHideScore?.Invoke();
            }
        }

        private void OnChangeColorType(ColorType _colorType) => colorType = _colorType;

        private ColorType GetColorType() => colorType;
        
        private void OnReset()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            
            _collectableList.Clear();
            _tempList.Clear();
            _collectableList.TrimExcess();
            _tempList.TrimExcess();
            _initializeStackOnStartCommand.OnInitializeStackOnStart(5);//test pupose that bind next level signal
        }
    }
}