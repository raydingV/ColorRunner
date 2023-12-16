using System.Collections;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region SelfVariables

        #region Private Variavles

        private SkinnedMeshRenderer _renderer;

        #endregion

        #region Serialize Variavles

        [SerializeField] CollectableManager _manager;

        #endregion

        #endregion

        private void Awake()
        {
            _renderer = GetComponent<SkinnedMeshRenderer>();
        }

        private void SetParticulColor(Color color)
        {

        }

        public void SetMatarial(Material material)
        {
            _renderer.material = material;
        }

        private void AcrivateOutlineTrasition(/*state*/)
        {

        }
    }
}