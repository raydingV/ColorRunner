using System;
using System.Collections.Generic;

namespace Data.ValueObject
{
    [Serializable]
    public class CityData
    {
        public int LevelNumber;
        public List<BuildingData> BuildingData;
    }
}