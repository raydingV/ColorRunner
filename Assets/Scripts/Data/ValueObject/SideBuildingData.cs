using System;
using Enums;

namespace Data.ValueObject
{
    [Serializable]
    public class SideBuildingData
    {
        public string BuildingName;

        public BuildingComplateState CompleteState = BuildingComplateState.Uncompleted;
        public int Price;
        public int PayedAmount;
    }
}