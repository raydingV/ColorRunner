using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;
using ValueObject;

namespace UnityObject
{
    [CreateAssetMenu(fileName = "CD_Structure", menuName = "ColorsRunners/Building", order = 0)]
    public class CD_Structure : ScriptableObject
    {
        public StructureData StructureData;
    }
}