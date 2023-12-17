using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class WayPointData
    {
        public List<Transform> Waypoints;
    }
}