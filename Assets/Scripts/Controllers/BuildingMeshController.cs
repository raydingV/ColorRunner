using System;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;
using MK.Toon;

namespace Controllers
{
    public class BuildingMeshController : MonoBehaviour
    {
        [SerializeField] private List<Renderer> renderers;
        
        private void Start()
        {
            // deneme amacli
            ChangeBuildingSaturation(0);
        }

        public void ChangeBuildingSaturation(float saturation)
        {
            foreach (var rend in renderers)
            {
                rend.material.DOFloat(saturation,"_Saturation", .5f);
            }
        }
    }
}