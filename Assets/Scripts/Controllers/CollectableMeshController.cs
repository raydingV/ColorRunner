using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;
using UnityObject;
using ValueObject;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region SelfVariables

        #region Private Variavles

        private ColorData _colorData;

        #endregion Private Variavles

        #region Serialize Variavles

        [SerializeField] private CollectableManager manager;

        #endregion Serialize Variavles

        #region Public Variables

        public Material Material;

        #endregion Public Variables

        #endregion SelfVariables

        private void Awake()
        {
            Material = GetComponent<SkinnedMeshRenderer>().material;
        }

        private void Start()
        {
            GetColorData();
            Material.color = _colorData.Color;
        }

        public void ChangeMatarialColor()
        {
            GetColorData();
            Material.color = _colorData.Color;
        }

        private void GetColorData() => _colorData = Resources.Load<CD_ColorData>("Data/CD_ColorData").Colors[(int)manager.currentColorType];

        public void SetCollectableMatarial(Material material)
        {
            Material.color = material.color;
        }

        public void ActivateOutlineTrasition(OutlineType type)
        {
            if (type == OutlineType.NonOutline)
            {
                Material.DOFloat(0, "_OutlineSize", 1f);
                return;
            }
            else if (type == OutlineType.Outline)
            {
                Material.DOFloat(100, "_OutlineSize", 1f);
                return;
            }
        }
    }
}