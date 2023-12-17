using System.Collections.Generic;
using UnityEngine;

namespace UnityObject
{
    [CreateAssetMenu(fileName = "CD_CityScriptableObject", menuName = "ColorsRunners/City", order = 0)]
    public class CD_CityScriptableObject : ScriptableObject
    {
        public List<CD_Structure> CityScriptableObject;
    }
}