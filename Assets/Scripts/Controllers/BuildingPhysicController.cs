using System.Collections;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class BuildingPhysicController : MonoBehaviour
    {

        [SerializeField] BuildingManager manager;

        private void OnTriggerStay(Collider other)
        {
            if(other.CompareTag("Player") && CompareTag("MainBuilding"))
            {
                
            }
            if (other.CompareTag("Player") && CompareTag("SideBuilding"))
            {

            }
        }

    }
}