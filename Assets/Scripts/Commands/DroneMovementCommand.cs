using DG.Tweening;
using Signals;
using UnityEngine;

namespace Commands
{
    public class DroneMovementCommand
    {
        private Transform _transform;
        private Sequence _sequence;
        private Transform[] _movePath;
        private float[] _moveSpeed;
        private float _delay;

        public DroneMovementCommand(Transform transform, Sequence sequence, ref Transform[] movePath, ref float[] moveSpeed, float delay)
        {
            _transform = transform;
            _sequence = sequence;
            _movePath = movePath;
            _moveSpeed = moveSpeed;
            _delay = delay;
        }

        public void Execute()
        {
            _sequence = DOTween.Sequence();
            _sequence.Pause();
            for (int i = 0; i < _movePath.Length; i++)
            {
                _sequence.Append(_transform.DOMove(_movePath[i].position, _moveSpeed[i]));
                _sequence.Join(_transform.DORotate(
                    new Vector3(_movePath[i].localEulerAngles.x, _movePath[i].localEulerAngles.y, _movePath[i].localEulerAngles.z),
                    _moveSpeed[i]));

                _sequence.SetDelay(_delay);
                _sequence.OnComplete(() =>
                {
                    StackSignals.Instance.onDroneAnimationComplated?.Invoke();
                    PlayerSignals.Instance.onDroneAnimationComplated?.Invoke();
                    ScoreSignals.Instance.onUpdateScoreAfterDroneArea?.Invoke();
                });
            }
            _sequence.Play();
        }
    }
}