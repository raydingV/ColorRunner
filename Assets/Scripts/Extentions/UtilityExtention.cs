using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Extentions
{
    public static class UtilityExtention
    {
        public static void SetAlpha(this Material material, float alpha)
        {
            Color _color = material.color;
            _color.a = alpha;
            material.color = _color;
        }
    }
}
