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

        private int _turretFireCounter;
        
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

        private void OnWrongTurretMatAreaEntered(Transform target)
        {
            _turretFireCounter++;
            int randomShoot = Random.Range(0, 2);
            
            if (_turretFireCounter % 2 == 0 && randomShoot == 1)
            {
                leftTurret.Aim(target);
                print("Left");
            }
            else if(_turretFireCounter % 2 != 0 && randomShoot != 1)
            {
                rightTurret.Aim(target);
                print("Right");
            }
        }
    }
}