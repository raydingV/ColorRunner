using System;
using Enums;
using System.Collections;
using DG.Tweening;
using Signals;
using UnityEngine;
using UnityObject;
using ValueObject;

namespace Controllers
{
    public class MatController : MonoBehaviour
    {
        #region SelfVariables

        #region Private Variavles

        private Material _material;
        private BoxCollider _boxCollider;
        #endregion

        #region Serialize Variavles


        #endregion

        #region Public Variables

        public ColorData ColorData;
        public ColorType currentColorType;

        #endregion

        #endregion

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
            _boxCollider = GetComponent<BoxCollider>();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion
        
        
        public void GetColorData(ColorType colorType) => ColorData = Resources.Load<CD_ColorData>("Data/CD_ColorData").Colors[(int)colorType];

        public void SetColorData(ColorType colorType)
        {
            currentColorType = colorType;
            _material.color = ColorData.Color;
        }

        public void DisableBoxCollider()
        {
            if(_boxCollider.enabled)
                _boxCollider.enabled = false;
        }
        
        public void CloseMat()
        {
            transform.DOScaleZ(.25f, .5f).OnComplete(() => { transform.DOScaleX(0, .5f).OnComplete(() =>
            { transform.DOScaleZ(0.01f, .1f);});});
        }

        private void OnReset()
        {
            _boxCollider.enabled = true;
            DOTween.KillAll();
        }
    }
}