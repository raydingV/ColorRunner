using System;
using Enums;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class MainBuildingData
    {
        public string BuildingName;
        
        public BuildingComplateState CompleteState = BuildingComplateState.Uncompleted;
        public int Price;
        public int PayedAmount;
    }
}