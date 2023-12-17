using System.Threading.Tasks;
using Controllers;
using Enums;
using Controllers;
using Signals;
using UnityEngine;

namespace Managers
{
    public class TurretAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion
        
        #region SeriliazeField
        
        [SerializeField] private TurretController leftTurret;
        [SerializeField] private TurretController rightTurret;
        [SerializeField] private TurretMatController[] matControllers;
        [SerializeField] private ColorType[] matColorTypes;
        
        #endregion

        #region Private Variables

        private int _turretFireCounter = 2;
        
        #endregion
        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onWrongTurretMatAreaEntered += OnWrongTurretMatAreaEntered;
        }

        private void UnSubscribeEvents()
        {
            StackSignals.Instance.onWrongTurretMatAreaEntered -= OnWrongTurretMatAreaEntered;
        }

        #endregion
        
        private void Awake()
        {
            SendColorTypeToMats();
        }

        private void Start()
        {
            SetColorOfMats();
        }

        #region SetMatsColors

        private void SendColorTypeToMats()
        {
            for (int i = 0; i < matControllers.Length; i++)
            {
                matControllers[i].GetColorData(matColorTypes[i]);
            }
        }
        
        private void SetColorOfMats()
        {
            for (int i = 0; i < matControllers.Length; i++)
            {
                matControllers[i].SetColorData(matColorTypes[i]);
            }
        }
        #endregion

        private async void OnWrongTurretMatAreaEntered(Transform target)
        {
            await Task.Delay(250);
            
            if (_turretFireCounter % 2 == 0)
            {
                leftTurret.Aim(target);
                rightTurret.Aim(target);
                rightTurret.Shoot(target);
            }

            await Task.Delay(250);
            _turretFireCounter++;
        }
    }
}