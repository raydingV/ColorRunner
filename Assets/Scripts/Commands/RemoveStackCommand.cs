using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signals;
using UnityEngine;

namespace Commands
{
    public class RemoveStackCommand
    {

        private List<Transform> _collectable;

        public RemoveStackCommand(ref List<Transform> collectable)
        {
            _collectable = collectable;
        }

        public void OnRemoveFromStack(Transform collectable)
        {
            _collectable.Remove(collectable);
            _collectable.TrimExcess();
            collectable.gameObject.SetActive(false); //after use with pool ?
            StackSignals.Instance.onCollectableRemovedFromStack?.Invoke();
            if(_collectable.Count == 0) return;
            StackSignals.Instance.onSetScoreControllerPosition?.Invoke(_collectable[0]);
        }
    }
}