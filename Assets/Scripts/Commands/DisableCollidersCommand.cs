using System;
using UnityEngine;

namespace Commands
{
    public class DisableCollidersCommand : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle") || other.CompareTag("Collectable"))
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}