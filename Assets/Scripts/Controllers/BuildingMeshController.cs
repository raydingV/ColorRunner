using System;
using DG.Tweening;
using Managers;
using UnityEngine;
using MK.Toon;

namespace Controllers
{
    public class BuildingMeshController : MonoBehaviour
    {
        [SerializeField] private Renderer rend;
        
        private void Start()
        {
            ChangeBuildingSaturation(0);
        }

        public void ChangeBuildingSaturation(float saturation)
        {
            rend.material.DOFloat(saturation,"_Saturation", .5f);
        }
    }
}