using Data.UnityObject;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    public class NpcPlayerPatrolController : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables

        [SerializeField] private List<Transform> _waypoints;
        [SerializeField] private float _patrolSpeed;

        #endregion Serialize Variables

        #region Private Variables

        private int _currentWaypointTarget;
        private Coroutine _prevCoroutine;

        #endregion Private Variables

        #endregion Self Variables

        private void Awake()
        {
            _patrolSpeed = 2f;
        }


        private void OnEnable()
        {
            _prevCoroutine = StartCoroutine(MovingToNextWaypoint());
        }

        private void OnDisable()
        {
            StopCoroutine(_prevCoroutine);
        }

        private IEnumerator MovingToNextWaypoint()
        {
            Transform wp = _waypoints[_currentWaypointTarget];
            transform.LookAt(wp.position);

            while (Vector3.Distance(transform.position, wp.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, wp.position, _patrolSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = wp.position;
            yield return new WaitForSeconds(1);
            _currentWaypointTarget = (_currentWaypointTarget + 1) % _waypoints.Count;

            StopCoroutine(_prevCoroutine);
            _prevCoroutine = StartCoroutine(MovingToNextWaypoint());
        }
    }
}