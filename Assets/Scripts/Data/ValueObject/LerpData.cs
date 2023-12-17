using System;
using System.Collections.Generic;
using UnityEngine;

namespace ValueObject
{
    [Serializable]
    public class LerpData
    {
        public Vector3 ScaleOffset = new Vector3(1.5f, 1.5f, 1.5f);
        public float DistanceOffSet = 0.6f;
        public Vector3 LerpSpeeds = new Vector3(7, 7, 5);
    }
}
