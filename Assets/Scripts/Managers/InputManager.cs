using System;
using System.Collections.Generic;
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


        #endregion

        #endregion


        private void Awake()
        {
            Data = GetInputData();
        }

        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;


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
            InputSignals.Instance.onJoystickStateChange += OnJoystickStateChange;
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
            InputSignals.Instance.onJoystickStateChange -= OnJoystickStateChange;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void OnPointerDown()
        {

        }

        private void OnPointerDragged()
        {
            JoystickMovement();
        }

        private void OnPointerReleased()
        {

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

        private void JoystickMovement()
        {
            float horizontal = floatingJoystick.Horizontal;
            float vertical = floatingJoystick.Vertical;

            InputSignals.Instance.onInputParamsUpdate?.Invoke(new InputParameters()
            {
                ValueOfX = horizontal,
                ValueOfY = vertical
            });
        }

        public void OnJoystickStateChange(JoystickStates joystickState)
        {
            switch (joystickState)
            {
                case JoystickStates.Runner:
                    floatingJoystick.AxisOptions = global::AxisOptions.Horizontal;
                    joystickHandleImg.enabled = false;
                    joystickBackgroundImg.enabled = false;
                    break;

                case JoystickStates.Idle:
                    floatingJoystick.AxisOptions = global::AxisOptions.Both;
                    joystickHandleImg.enabled = true;
                    joystickBackgroundImg.enabled = true;
                    break;
            }
        }

        private bool IsPointerOverUIElement()
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