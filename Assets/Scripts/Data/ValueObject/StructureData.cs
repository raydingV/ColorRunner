using System;
using Enums;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class StructureData
    {
        [Header("BuildingType")]
        public BuildType BuildingType;
        [Space]
        [Header("Main")]
        public string MainBuildingName;
        public BuildingComplateState MainCompleteState = BuildingComplateState.Uncompleted;
        public int MainPrice;
        public int MainPayedAmount;
        public int MainPayedAmountText;

        [Header("Side")]
        public string SideBuildingName;
        public BuildingUnlockState SideUnlockState = BuildingUnlockState.Locked;
        public BuildingComplateState SideCompleteState = BuildingComplateState.Uncompleted;
        public int SidePrice;
        public int SidePayedAmount;
        public int SidePayedAmountText;
    }
}