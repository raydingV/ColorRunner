using Enums;
using System.Collections;
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

        public void GetColorData(ColorType colorType) => ColorData = Resources.Load<CD_ColorData>("Data/CD_ColorData").Colors[(int)colorType];

        public void SetColorData(ColorType colorType)
        {
            currentColorType = colorType;
            _material.color = ColorData.Color;
        }

        public void DisableBoxCollider()
        {
            _boxCollider.enabled = false;
        }
    }
}