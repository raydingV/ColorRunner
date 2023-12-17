using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Commands
{
    public class AddCollectablesAfterDroneAnimationDoneCommand
    {

        private List<Transform> _collectable;
        private List<Transform> _tempList;

        public AddCollectablesAfterDroneAnimationDoneCommand(ref List<Transform> collectable, ref List<Transform> tempList)
        {
            _collectable = collectable;
            _tempList = tempList;
        }   

        public void OnAddCollectablesAfterDroneAnimationDone(bool isDead, Transform _tranform)
        {
            if (isDead)
            {
                _tempList.Remove(_tranform);
                _tempList.TrimExcess();
            }

            else if (!isDead && _tempList.Contains(_tranform))
            {
                _tempList.Remove(_tranform);
                _collectable.Add(_tranform);
                _tempList.TrimExcess();
                _collectable.TrimExcess();
                StackSignals.Instance.onSetStackStartSize?.Invoke(1);
                StackSignals.Instance.onSetScoreControllerPosition?.Invoke(_collectable[0]);
            }
        }
    }
}
