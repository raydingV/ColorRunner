using DG.Tweening;
using Enums;
using Managers;
using System.Threading.Tasks;
using UnityEngine;
using UnityObject;
using ValueObject;
using static UnityEngine.ParticleSystem;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {

        #region SelfVariables

        #region Public Variables

        public Material Material;

        #endregion Public Variables

        #region Serialize Variavles

        [SerializeField] private CollectableManager manager;
        [SerializeField] private ParticleSystem particle;


        #endregion Serialize Variavles

        #region Private Variavles

        private MainModule _mainModule;
        private ColorData _colorData;
        private Renderer _renderer;
        private Material _rainbow;
        #endregion Private Variavles

        #endregion SelfVariables

        private void Awake()
        {
            Material = GetComponent<SkinnedMeshRenderer>().material;
            _renderer = GetComponent<Renderer>();
            _mainModule = particle.main;
        }

        private void Start()
        {
            GetColorData();
            GetRainbowMat();
            Material.color = _colorData.Color;
        }

        public void ChangeMatarialColor()
        {
            GetColorData();
            Material.color = _colorData.Color;
        }

        private void GetColorData() => _colorData = Resources.Load<CD_ColorData>("Data/CD_ColorData").Colors[(int)manager.currentColorType];

        private void GetRainbowMat() => _rainbow = Resources.Load<Material>("RainbowMaterial/RainbowMaterial");


        public async void SetCollectableMatarial(Material material)
        {
            Material.color = material.color;
            await Task.Yield();
        }

        public void SetRainbowMaterial()
        {
            GetComponent<SkinnedMeshRenderer>().material = _rainbow;
        }

        public void StartParticle()
        {
            var emitParams = new EmitParams();
            emitParams.startColor = _renderer.material.color;
            emitParams.startSize = particle.startSize / 2;
            particle.Emit(emitParams, 1);
            particle.Play();
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