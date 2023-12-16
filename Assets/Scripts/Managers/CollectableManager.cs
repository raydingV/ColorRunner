using System;
using Controllers;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {

        #region SelfVariables

        #region Serialize Variables

        [SerializeField] CollectableMeshController meshController;
        [SerializeField] CollectablePhisicController phisicController;
        [SerializeField] CollectableAnimationController animatorController;

        #endregion

        #region Private Variables
        //write collectable State
        private Material _playerMat;

        #endregion

        #endregion

        private void Awake()
        {
            _playerMat = GameObject.FindObjectOfType<PlayerMeshController>().GetComponent<SkinnedMeshRenderer>()
                .material;
        }

        #region Subscriptions

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            PlayerSignals.Instance.onChangeMaterial += OnSetMaterial;
        }

        private void UnSubscribe()
        {

            PlayerSignals.Instance.onChangeMaterial -= OnSetMaterial;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        #endregion

        private void OnSetMaterial(Material material)
        {
            meshController.SetMatarial(_playerMat);
        }

        public void RotateMeshForward()
        {
            animatorController.transform.rotation = new Quaternion(0, 0, 0,0);
        }
    }
}
