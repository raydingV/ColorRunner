using System;
using System.Collections.Generic;
using Commands;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion

        #region Serialized Variables

        [SerializeField] private bool isReadyForTouch, isFirstTimeTouchTaken;
        [SerializeField] private FloatingJoystick floatingJoystick;
        [SerializeField] private Image joystickBackgroundImg;
        [SerializeField] private Image joystickHandleImg;

        #endregion

        #region Private Variables

        private JoystickMovementCommand _joystickMovementCommand;
        private JoystickStateChangeCommand _onJoystickStateChangeCommand;
        private StopJoystickMovementCommand _stopJoystickMovementCommand;

        #endregion

        #endregion


        private void Awake()
        {
            Data = GetInputData();
            _joystickMovementCommand = new JoystickMovementCommand(ref floatingJoystick);
            _onJoystickStateChangeCommand = new JoystickStateChangeCommand(ref floatingJoystick, ref joystickHandleImg,
                ref joystickBackgroundImg);
            _stopJoystickMovementCommand = new StopJoystickMovementCommand();
        }
        
        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            InputSignals.Instance.onPointerDown += OnPointerDown;
            InputSignals.Instance.onPointerDragged += OnPointerDragged;
            InputSignals.Instance.onPointerReleased += OnPointerReleased;
            InputSignals.Instance.onJoystickStateChange += _onJoystickStateChangeCommand.OnJoystickStateChange;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
        }
        
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
            InputSignals.Instance.onPointerDown -= OnPointerDown;
            InputSignals.Instance.onPointerDragged -= OnPointerDragged;
            InputSignals.Instance.onPointerReleased -= OnPointerReleased;
            InputSignals.Instance.onJoystickStateChange -= _onJoystickStateChangeCommand.OnJoystickStateChange;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()//Test version
        {
            InputSignals.Instance.onJoystickStateChange?.Invoke(JoystickStates.Runner);
        }

        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;


        
        private void OnPointerDown()
        {
            
        }
        
        private void OnPointerDragged()
        {
            _joystickMovementCommand.JoystickMovement();
        }
        
        private void OnPointerReleased()
        {
            _stopJoystickMovementCommand.StopJoystickMovement();
        }
        
        private void OnEnableInput()
        {
            isReadyForTouch = true;
        }

        private void OnDisableInput()
        {
            isReadyForTouch = false;
        }

        private void OnPlay()
        {
            isReadyForTouch = true;
        }
        
        private bool IsPointerOverUIElement() // Unused
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }

        private void OnReset()
        {
            InputSignals.Instance.onJoystickStateChange?.Invoke(JoystickStates.Runner);
        }
    }
}