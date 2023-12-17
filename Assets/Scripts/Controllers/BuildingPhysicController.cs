using System;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class BuildingPhysicController : MonoBehaviour
    {
        [SerializeField] private BuildingManager manager;
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.OnPlayerEnter();
            }
        }
    }
}