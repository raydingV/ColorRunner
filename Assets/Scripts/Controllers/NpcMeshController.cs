using Assets.Scripts.Managers;
using UnityEngine;
using UnityObject;
using ValueObject;

namespace Assets.Scripts.Controllers
{
    public class NpcMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables

        [SerializeField] private NpcManager manager;

        #endregion Serialize Variables

        #region Private Variables

        private Material _matarial;
        private ColorData _colorData;

        #endregion Private Variables

        #endregion Self Variables

        private void Awake()
        {
            _matarial = GetComponent<SkinnedMeshRenderer>().material;
        }

        private void Start()
        {
            GetColorData();
            _matarial.color = _colorData.Color;
        }


        private void GetColorData() => _colorData = Resources.Load<CD_ColorData>("Data/CD_ColorData").Colors[(int)manager.currentColorType];

        public void ChangeMatarialColor()
        {
            GetColorData();
            _matarial.color = _colorData.Color;
        }
    }
}