using ValueObject;
using UnityEngine;

namespace UnityObject
{
    [CreateAssetMenu(fileName = "CD_Lerp", menuName = "ColorsRunners/CD_Lerp", order = 0)]
    public class CD_Lerp : ScriptableObject
    {
        public LerpData Data = new LerpData();
    }
}
