using Enums;
using System;
using UnityEngine;

namespace ValueObject
{
    [Serializable]
    public class BuildingData
    {
        public string Name;
        public BuildType BuildingType;
        public SideBuildingType SideType;
        public BuildingUnlockState BuildingUnlockState;
        public BuildingComplateState ComplateState = BuildingComplateState.Uncompleted;
        public Color32 Color;
        public int Price;
        public int PayedAmount;
        public int PayedAmountText;
    }
}
