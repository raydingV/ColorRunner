using System.Collections.Generic;
using UnityEngine;
using ValueObject;

namespace Commands
{
    public class StackLerpMoveCommand
    {
        private List<Transform> _collectable;
        private LerpData _lerpData;
        private Transform _playerPossition;

        public StackLerpMoveCommand(ref List<Transform> collectable, ref LerpData lerpData, Transform playerPossition)
        {
            _collectable = collectable;
            _lerpData = lerpData;
            _playerPossition = playerPossition;
        }

        public void OnLerpStackMove()
        {
            if (_collectable.Count > 0)
            {
                //note that canbe put inside loop and perfectly fine just iteration number is inrease
                //put pack to stack behind the player
                _collectable[0].localPosition = new Vector3(
                    Mathf.Lerp(_collectable[0].localPosition.x, _playerPossition.localPosition.x,
                        _lerpData.LerpSpeeds.x * Time.deltaTime),
                    Mathf.Lerp(_collectable[0].localPosition.y, _playerPossition.localPosition.y,
                        _lerpData.LerpSpeeds.y * Time.deltaTime),
                    Mathf.Lerp(_collectable[0].localPosition.z, _playerPossition.localPosition.z - .5f,
                        _lerpData.LerpSpeeds.z * Time.deltaTime)
                );
                _collectable[0].LookAt(_playerPossition);

                //after each stack flow each other by n flow n - 1 prenciple by give offset and time
                for (int i = 1; i < _collectable.Count; i++)
                {
                    _collectable[i].localPosition = new Vector3(
                        Mathf.Lerp(_collectable[i].localPosition.x, _collectable[i - 1].localPosition.x,
                            _lerpData.LerpSpeeds.x * Time.deltaTime),
                        Mathf.Lerp(_collectable[i].localPosition.y, _collectable[i - 1].localPosition.y,
                            _lerpData.LerpSpeeds.y * Time.deltaTime),
                        Mathf.Lerp(_collectable[i].localPosition.z,
                            _collectable[i - 1].localPosition.z - _lerpData.DistanceOffSet,
                            _lerpData.LerpSpeeds.z * Time.deltaTime)
                    );
                    _collectable[i].LookAt(_playerPossition);
                }
            }
        }
    }
}