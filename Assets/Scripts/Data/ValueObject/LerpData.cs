using System;
using System.Collections.Generic;
using UnityEngine;

namespace ValueObject
{
    [Serializable]
    public class LerpData
    {
        public float ScaleOffset = 1.3f;
        public float DistanceOffSet = 1.7f;
        public Vector3 LerpSpeeds = new Vector3(1, 1, 1);
    }
}
