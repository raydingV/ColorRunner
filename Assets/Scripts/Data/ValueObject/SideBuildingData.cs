using System;
using Enums;
using Sirenix.OdinInspector;

namespace Data.ValueObject
{
    [Serializable]
    public class SideBuildingData
    {
        public string BuildingName;
        [EnumToggleButtons]
        public BuildingComplateState CompleteState = BuildingComplateState.Uncompleted;
        public int Price;
        public int PayedAmount;
    }
}