using System;
using System.Collections.Generic;
using ValueObject;
using UnityEngine;

namespace UnityObject
{
    [CreateAssetMenu(fileName = "CD_EnvironmentData", menuName = "ColorsRunners/CD_EnvironmentData", order = 0)]
    public class CD_EnvironmentData : ScriptableObject
    {
        public List<EnvironmentData> EnvironmentData;
    }
}
