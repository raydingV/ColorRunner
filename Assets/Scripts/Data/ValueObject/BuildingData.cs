using System;
using Enums;

namespace Data.ValueObject
{
    [Serializable]
    public class BuildingData
    {
        public BuildingType BuildingType;
        public MainBuildingData mainBuildingData;
        public SideBuildingData sideBuildindData;
    }
}