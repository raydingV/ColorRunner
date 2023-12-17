using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class DroneController : MonoBehaviour
    {
        [SerializeField] private Transform[] movePath;
        [SerializeField] private float[] moveSpeed;
        [SerializeField] private float delay;

        private Sequence _sequence;

        private void Start()
        {
            SetDroneMovement();
        }

        private void SetDroneMovement()
        {
            _sequence = DOTween.Sequence();
            for (int i = 0; i < movePath.Length; i++)
            {
                _sequence.Append(transform.DOMove(movePath[i].position, moveSpeed[i]));
                _sequence.Join(transform.DORotate(
                    new Vector3(movePath[i].localEulerAngles.x, movePath[i].localEulerAngles.y, movePath[i].localEulerAngles.z),
                    moveSpeed[i]));
                _sequence.SetDelay(delay);
            }
            _sequence.Pause();
        }
        
        public void StartDroneAnimation()
        {
            _sequence.Play();

            _sequence.OnComplete(() =>
            {
                StackSignals.Instance.onDroneAnimationComplated?.Invoke();
                PlayerSignals.Instance.onPlayerExitDroneArea?.Invoke();
            });
        }
    }
}