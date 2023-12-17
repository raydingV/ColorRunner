using System.Collections.Generic;
using Enums;

namespace Keys
{
    public struct SaveIdleGameDataParams
    {
        public int IdleLevel;
        public int CollectablesCount;
        public List<int> MainPayedAmount;
        public List<int> SidePayedAmount;
        public List<BuildingComplateState> MainBuildingState;
        public List<BuildingComplateState> SideBuildingState;
    }
}