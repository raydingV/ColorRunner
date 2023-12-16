using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signals;
using UnityEngine;

namespace Managers
{
    public class MatarialManager : MonoBehaviour
    {
        #region Self Variables
        #endregion

        #region Subscriptions

        private void OnEnable()
        {
            Subscription();
        }

        private void Subscription()
        {
            ColorSignals.Instance.onSetMarialRandomColor += OnSetMarialRandomColor;
            ColorSignals.Instance.onSetMarialGradiensColor += OnSetMarialGradiensColor;
        }

        private void UnSubscription()
        {
            ColorSignals.Instance.onSetMarialRandomColor -= OnSetMarialRandomColor;
            ColorSignals.Instance.onSetMarialGradiensColor -= OnSetMarialGradiensColor;
        }

        private void OnDisable()
        {
            UnSubscription();
        }

        #endregion

        private void GetMatarialData()
        {

        }

        private void OnSetMarialRandomColor(/*state*/)
        {

        }

        private void OnSetMarialGradiensColor()
        {
            
        }

    }
}
