using System;
using System.Collections.Generic;
using Enums;
using Data.ValueObject;

namespace Commands
{
    public class LoadGameCommand<T>
    {
        public static T OnLoadGameData (SaveStates state)
        {
            switch(state)
            {
                case SaveStates.Score: return (T)Convert.ChangeType(ES3.Load<T>("Score"),typeof(T));break;
                case SaveStates.Level: return (T)Convert.ChangeType(ES3.Load<T>("Level"),typeof(T));break;
            }
            return default;
        }
        public static T OnLoadGameData (SaveStates state, StructureData structureData)
        {
            switch(state)
            {
                case SaveStates.BuildingType : return (T)Convert.ChangeType(ES3.Load<T>(structureData.BuildingType.ToString()),typeof(T));
                case SaveStates.MainBuildingName: return (T)Convert.ChangeType(ES3.Load<T>(structureData.MainBuildingName.ToString()),typeof(T));break;
                case SaveStates.MainCompleteState: return (T)Convert.ChangeType(ES3.Load<T>(structureData.MainCompleteState.ToString()),typeof(T));break;
                case SaveStates.MainPrice: return (T)Convert.ChangeType(ES3.Load<T>(structureData.MainPrice.ToString()),typeof(T));break;
                case SaveStates.MainPayedAmount: return (T)Convert.ChangeType(ES3.Load<T>(structureData.MainPayedAmount.ToString()),typeof(T));break;
                case SaveStates.SideBuildingName: return (T)Convert.ChangeType(ES3.Load<T>(structureData.SideBuildingName.ToString()),typeof(T));break;
                case SaveStates.SideUnlockState: return (T)Convert.ChangeType(ES3.Load<T>(structureData.SideUnlockState.ToString()),typeof(T));break;
                case SaveStates.SideCompleteState: return (T)Convert.ChangeType(ES3.Load<T>(structureData.SideCompleteState.ToString()),typeof(T));break;
                case SaveStates.SidePrice: return (T)Convert.ChangeType(ES3.Load<T>(structureData.SidePrice.ToString()),typeof(T));break;
                case SaveStates.SidePayedAmount: return (T)Convert.ChangeType(ES3.Load<T>(structureData.SidePayedAmount.ToString()),typeof(T));break;
            }
            return default;
        }
    }
}