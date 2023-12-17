using System;
using System.Collections.Generic;
using Commands;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using StateMachine;
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
        [SerializeField] private GameStates currentGameState;
        #endregion
        
        #region Private Variables

        private bool _isTouching;
        private float _currentVelocity;
        private Vector2? _mousePosition;
        private Vector3 _moveVector;
        private Vector3 _joystickPosition;

        #endregion

        #endregion
        
        private void Awake()
        {
            Data = GetInputData();
        }
        
        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        void Update()
        {
            if (!isReadyForTouch) return;
            if (Input.GetMouseButtonUp(0)) 
            {
                _isTouching = false;
                InputSignals.Instance.onInputReleased?.Invoke();
            }
                
            if (Input.GetMouseButtonDown(0))
            {
                _isTouching = true;
                InputSignals.Instance.onInputTaken?.Invoke();
                if (!isFirstTimeTouchTaken)
                {
                    isFirstTimeTouchTaken = true;
                    InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                }

                _mousePosition = Input.mousePosition;
            }

            if (currentGameState == GameStates.Runner)
            {
                if (Input.GetMouseButton(0))
                {
                    if (_isTouching)
                    {
                        if (_mousePosition != null)
                        {
                            Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;

                            if (mouseDeltaPos.x > Data.HorizontalInputSpeed)
                                _moveVector.x = Data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                            else if (mouseDeltaPos.x < -Data.HorizontalInputSpeed)
                                _moveVector.x = -Data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
                            else
                                _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                                    Data.ClampSpeed);

                            _mousePosition = Input.mousePosition;

                            InputSignals.Instance.onInputDragged?.Invoke(new RunnerInputParams()
                            {
                                XValue = _moveVector.x,
                                ClampValues = new Vector2(Data.ClampSides.x, Data.ClampSides.y)
                            });
                        }
                    }
                }
            }

            if (currentGameState == GameStates.Idle)
            {
                if (Input.GetMouseButton(0))
                {
                    if (_isTouching)
                    {
                        if (currentGameState == GameStates.Idle)
                        {
                            _joystickPosition = new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical);
                            
                            _moveVector = _joystickPosition;
                            
                            InputSignals.Instance.onJoystickDragged?.Invoke(new IdleInputParams()
                            {
                                joystickMovement = _moveVector
                            });
                        }
                    }
                }
            }
        }

        private void OnChangeGameState(GameStates currentStates)
        {
            currentGameState = currentStates;
            
            if (currentGameState == GameStates.Idle)
            {
                floatingJoystick.gameObject.SetActive(true);
            }
            else
            {
                floatingJoystick.gameObject.SetActive(false);
            }
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
            
        private void OnReset()
        {
            _isTouching = false;
            isReadyForTouch = false;
            isFirstTimeTouchTaken = false;
        }
    }
}