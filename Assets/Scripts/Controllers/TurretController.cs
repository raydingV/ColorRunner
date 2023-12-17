using System.Threading.Tasks;
using DG.Tweening;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class TurretController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Vector2 turretMaxAngleX;
        [SerializeField] private Vector2 turretMaxAngleY;
        [SerializeField] private GameObject turret;
        [SerializeField] private ParticleSystem fireParticle;
        [SerializeField] private float turnSpeed;
        [SerializeField] private float turnDelay;
        private bool _isAiming, _isRotating ,_canShoot;
        #endregion

        #region EventSubscription

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
            PlayerSignals.Instance.onPlayerExitTurretArea += DeactivateTurret;
        }

        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerExitTurretArea -= DeactivateTurret;
        }

        #endregion
        
        
        private void Start()
        {
            _canShoot = true;
            Idle();
        }
        
        private void Idle()
        {
            if (!_isAiming && !_isRotating)
            {
                _isRotating = true;
                float randomRotateX = Random.Range(turretMaxAngleX.x, turretMaxAngleX.y);
                float randomRotateY = Random.Range(turretMaxAngleY.x, turretMaxAngleY.y);
                Vector3 randomRotationVector = new Vector3(randomRotateX, randomRotateY - 180f, 0);
                turret.transform.DORotate(randomRotationVector, turnSpeed).SetDelay(turnDelay * 2).OnComplete(() => {
                    _isRotating = false; Idle();});
            }
        }
        
        public async void Aim(Transform target)
        {
            if (!_canShoot) return;

            await Task.Delay(200);
            
            _isAiming = true;
            turret.transform.DOLookAt(target.position, turnSpeed).SetDelay(turnDelay * 1.66f).OnComplete(() =>
            {
                fireParticle.Play();
                StackSignals.Instance.onRemoveFromStack?.Invoke(target);
            });
        }

        private void DeactivateTurret()
        {
            _canShoot = false;
        }
    }
}