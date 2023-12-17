using System.Collections;
using Extentions;
using Managers;
using ValueObject;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class BuildingMeshController : MonoBehaviour
    {

        [SerializeField] BuildingManager manager;
        private BuildingComplateState _complateState;
        private Material _material;

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
        }

        private void Start()
        {
            _material.SetAlpha(0);
        }

        public void OpenAlpha()
        {
            _material.SetAlpha(1);
        }

        public void GetData(BuildingComplateState complateState)
        {
            _complateState = complateState;
        }
    }
}