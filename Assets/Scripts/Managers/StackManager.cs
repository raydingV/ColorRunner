using System;
using System.Collections.Generic;
using System.Linq;
using Signals;
using DG.Tweening;
using UnityObject;
using ValueObject;
using UnityEngine;
using System.Collections;
using Controllers;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region SelfVariables

        #region privateVariables

        [SerializeField] List<Transform> _collectable = new List<Transform>();
        private Transform _playerPossition;
        private Material _playerMat;
        private LerpData _lerpData;

        #endregion

        #endregion

        

        //temp
        private void Awake()
        {
            _playerPossition = GameObject.FindGameObjectWithTag("Player").transform;

            _lerpData = GetLerpData();
            Debug.Log(_lerpData.LerpSpeeds);
        }

        private void FixedUpdate()
        {
            OnLerpStackMove();
        }

        private LerpData GetLerpData() => Resources.Load<CD_Lerp>("Data/CD_Lerp").Data;

        #region Subscriptions

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            StackSignals.Instance.onAddStack += OnAddStack;
            StackSignals.Instance.OnRemoveFromStack += OnRemoveFromStack;
            StackSignals.Instance.OnLerpStack += OnLerpStackMove;
            StackSignals.Instance.OnSetStackStartSize += OnSetStackStartSize;
            //StackSignals.Instance.OnShakeStackSize += OnShakeStackSize;
            StackSignals.Instance.OnThrowStackInMiniGame += OnThrowStackInMiniGame;
        }

        private void UnSubscribe()
        {
            StackSignals.Instance.onAddStack -= OnAddStack;
            StackSignals.Instance.OnRemoveFromStack -= OnRemoveFromStack;
            StackSignals.Instance.OnLerpStack -= OnLerpStackMove;
            StackSignals.Instance.OnSetStackStartSize -= OnSetStackStartSize;
            //StackSignals.Instance.OnShakeStackSize -= OnShakeStackSize;
            StackSignals.Instance.OnThrowStackInMiniGame -= OnThrowStackInMiniGame;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        #endregion

       
        private void OnAddStack(Transform collectable)
        {
            collectable.tag = "Collected";
            collectable.SetParent(transform);
            _collectable.Add(collectable);
            _collectable.TrimExcess();
            StartCoroutine(OnShakeStackSize());
        }

        private void OnRemoveFromStack(Transform collectable)
        {
            _collectable.Remove(collectable);
            _collectable.TrimExcess();
        }

        private void OnSetStackStartSize(int size) 
        {
        }

        private void OnLerpStackMove() 
        {
            
            if (_collectable.Count > 0)
            {
                // note that canbe put inside loop and perfectly fine just iteration number is inrease
                //put pack to stack behind the player 
                _collectable[0].localPosition = new Vector3(
                    Mathf.Lerp(_collectable[0].localPosition.x, _playerPossition.localPosition.x, 10f * Time.deltaTime),
                    Mathf.Lerp(_collectable[0].localPosition.y, _playerPossition.localPosition.y, 10f * Time.deltaTime),
                    Mathf.Lerp(_collectable[0].localPosition.z, _playerPossition.localPosition.z - _lerpData.DistanceOffSet, 10f * Time.deltaTime)
                    );

                //after each stack flow each other by n flow n - 1 prenciple by give offset and time 
                for(int i = 1; i < _collectable.Count; i++)
                {
                    _collectable[i].localPosition = new Vector3(
                         Mathf.Lerp(_collectable[i].localPosition.x, _collectable[i - 1].localPosition.x, 10f * Time.deltaTime),
                         Mathf.Lerp(_collectable[i].localPosition.y, _collectable[i - 1].localPosition.y, 100f * Time.deltaTime),
                         Mathf.Lerp(_collectable[i].localPosition.z, _collectable[i - 1].localPosition.z - _lerpData.DistanceOffSet, 10f * Time.deltaTime)
                         );

                }
                
            }
        }

        private IEnumerator OnShakeStackSize() 
        {
            _collectable.TrimExcess();

            for (int i = 0; i < _collectable.Count; i++)
            {
                if (i < 0 || i >= _collectable.Count)
                {
                    yield break;
                }
                _collectable[i].transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.12f).SetEase(Ease.Flash);
                _collectable[i].transform.DOScale(Vector3.one, 0.12f).SetDelay(0.12f).SetEase(Ease.Flash);

                _collectable.TrimExcess();
                yield return new WaitForSeconds(0.05f);
            }

        }

        private void OnJumpOnJumpPlatform()
        {

        }

        private void OnThrowStackInMiniGame() 
        {
        }

        private void SortStack()
        {
        }
    }
}
