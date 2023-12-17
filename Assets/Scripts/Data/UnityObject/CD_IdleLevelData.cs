using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace UnityObject
{
    [CreateAssetMenu(fileName = "CD_IdleLevelData", menuName = "ColorsRunners/IdleLevelData", order = 0)]
    public class CD_IdleLevelData : ScriptableObject
    {
        public IdleLevelData IdleLevel;
    }
}