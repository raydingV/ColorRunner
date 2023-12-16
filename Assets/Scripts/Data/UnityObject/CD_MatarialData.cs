using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueObject;
using UnityEngine;

namespace UnityObject
{
    [CreateAssetMenu(fileName = "CD_MatarialData", menuName = "ColorRunner/CD_MatarialData", order = 0)]
    public class CD_MatarialData : ScriptableObject
    {
        [SerializeField] List<MatarialData> matarials;
    }
}
