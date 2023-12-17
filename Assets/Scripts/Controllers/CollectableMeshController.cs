using System.Collections;
using Managers;
using MK.Toon;
using DG.Tweening;
using Enums;
using UnityObject;
using ValueObject;
using UnityEngine;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region SelfVariables

        #region Private Variavles

        private ColorData _colorData;

        #endregion

        #region Serialize Variavles

        [SerializeField] CollectableManager manager;

        #endregion


        #region Public Variables

        public Material _material;


        #endregion

        #endregion

        private void Awake()
        {
            _material = GetComponent<SkinnedMeshRenderer>().material;
           
        }

        private void Start()
        {
            GetColorData();
            _material.color = _colorData.Color;
        }

        public void ChangeMatarialColor()
        {

            GetColorData();
            _material.color = _colorData.Color;
        }

        private void GetColorData() => _colorData = Resources.Load<CD_ColorData>("Data/CD_ColorData").Colors[(int)manager.currentColorType];

        public void SetCollectableMatarial(Material material)
        {
            _material.color = material.color;
        }

        public void ActivateOutlineTrasition(OutlineType type)
        {
            if(type == OutlineType.NonOutline)
            {
                _material.DOFloat(0, "_OutlineSize", 1f);
            }
            else if( type == OutlineType.Outline)
            {
                _material.DOFloat(100, "_OutlineSize", 1f);
            }
        }
    }
}