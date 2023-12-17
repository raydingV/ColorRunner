using Data.ValueObject;
using System.Linq;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_WaypointData", menuName = "ColorsRunners/WaypointDatas", order = 0)]
    public class CD_WaypointData : ScriptableObject
    {
        public WayPointData waypoint;
    }
}