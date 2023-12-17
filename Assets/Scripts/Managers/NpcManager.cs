 using Assets.Scripts.Controllers;
using DG.Tweening;
using Enums;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class NpcManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorType currentColorType;

        #endregion Public Variables

        #region Serialize Variables
        [SerializeField] private NpcPhysicController physicController;
        [SerializeField] private NpcMeshController meshController;

        #endregion Serialize Variables

        #endregion Self Variables

        public void OnChangeMatarialColor(ColorType type)
        {
            currentColorType = type;
            meshController.ChangeMatarialColor();
        }

        public void Rotate() => transform.DORotate(new Vector3(0, 180, 0), 0.2f);
    }
}