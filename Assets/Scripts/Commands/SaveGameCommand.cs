using Data.ValueObject;
using Enums;

namespace Commands
{
    public class SaveGameCommand<T>
    {
        public void OnSaveGameData(SaveStates state, T data, StructureData structureData)
        {
            switch(state)
            {
                case SaveStates.Score: ES3.Save("Score", data);break;
                case SaveStates.Level: ES3.Save("Level", data);break;
                case SaveStates.BuildingType: ES3.Save(structureData.BuildingType.ToString(),data);break;
                case SaveStates.MainBuildingName: ES3.Save(structureData.MainBuildingName, data);break;
                case SaveStates.MainCompleteState: ES3.Save(structureData.MainCompleteState.ToString(), data);break;
                case SaveStates.MainPrice: ES3.Save(structureData.MainPrice.ToString(), data);break;
                case SaveStates.MainPayedAmount: ES3.Save(structureData.MainPayedAmount.ToString(), data);break;
                case SaveStates.SideBuildingName: ES3.Save(structureData.SideBuildingName, data);break;
                case SaveStates.SideUnlockState: ES3.Save(structureData.SideUnlockState.ToString(), data);break;
                case SaveStates.SideCompleteState: ES3.Save(structureData.SideCompleteState.ToString(), data);break;
                case SaveStates.SidePrice: ES3.Save(structureData.SidePrice.ToString(), data);break;
                case SaveStates.SidePayedAmount: ES3.Save(structureData.SidePayedAmount.ToString(), data);break;
            }
        }
    }
}