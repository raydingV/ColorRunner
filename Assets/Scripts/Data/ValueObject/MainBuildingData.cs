using System;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class MainBuildingData
    {
        public string BuildingName;
        [EnumToggleButtons]
        public BuildingComplateState CompleteState = BuildingComplateState.Uncompleted;
        public int Price;
        public int PayedAmount;
    }
}