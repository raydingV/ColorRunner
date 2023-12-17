using DG.Tweening;
using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;
using Managers;
using UnityEngine;

namespace Commands
{
    public class StackEnterDroneAreaCommand
    {
        private List<Transform> _collectable;
        private List<Transform> _tempList;
        
        public StackEnterDroneAreaCommand(ref List<Transform> collectable, ref List<Transform> tempList)
        {
            _collectable = collectable;
            _tempList = tempList;
        }

        public void OnStackEnterDroneArea(Transform collectable, Transform mat)
        {
            if (!_collectable.Contains(collectable)) return;
            _tempList.Add(collectable);
            _collectable.Remove(collectable);
            _collectable.TrimExcess();
            _tempList.TrimExcess();
            collectable.DOMove(
                    new Vector3(mat.position.x, collectable.position.y,
                        collectable.position.z + UnityEngine.Random.Range(6, 10)), 3f)
                .OnComplete(() => collectable.GetComponent<CollectableManager>().OnTranslateAnimationState(new SneakIdleAnimationState()));

            if (_collectable.Count == 0)
            {
                StackSignals.Instance.onLastCollectableEnterDroneArea?.Invoke();
            }
        }
    }
}
