using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ValueObject;

namespace Commands
{
    public class ShakeStackCommand
    {
        private List<Transform> _collectable;
        private LerpData _lerpData;

        public ShakeStackCommand(ref List<Transform> collectable, ref LerpData lerpData)
        {
            _collectable = collectable;
            _lerpData = lerpData;
        }

        public IEnumerator ShakeStackSize()
        {
            _collectable.TrimExcess();

            for (int i = 0; i < _collectable.Count; i++)
            {
                if (i < 0 || i >= _collectable.Count)
                {
                    yield break;
                }
                _collectable[i].transform.DOScale(new Vector3(_lerpData.ScaleOffset.x, _lerpData.ScaleOffset.y, _lerpData.ScaleOffset.z), 0.12f).SetEase(Ease.Flash);
                _collectable[i].transform.DOScale(Vector3.one, 0.12f).SetDelay(0.12f).SetEase(Ease.Flash);
                _collectable.TrimExcess();
                yield return new WaitForSeconds(0.05f);
            }

        }
    }
}
