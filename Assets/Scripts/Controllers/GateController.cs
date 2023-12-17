using System.Collections;
using Enums;
using Managers;
using Extentions;
using UnityEngine;
using UnityObject;
using ValueObject;

namespace Controllers
{
    public class GateController : MonoBehaviour
    {
        #region SelfVariables

        #region Public Variables

        public ColorType currentColorType;

        #endregion
        
        #region Serialize Variavles


        #endregion
        
        #region Private Variavles

        private Material _material;
        public ColorData _colorData;

        #endregion
        
        #endregion

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
            GetColorData();
            _material.color = _colorData.Color;
            // _material.SetAlpha(0.5f);
        }

        private void GetColorData() => _colorData = Resources.Load<CD_ColorData>("Data/CD_ColorData").Colors[(int)currentColorType];
    }
}