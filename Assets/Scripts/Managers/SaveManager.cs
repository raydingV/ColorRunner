using System;
using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using UnityEngine;
using UnityObject;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        //private LoadGameCommand _loadGameCommand;
        //private SaveGameCommand _saveGameCommand;
        [SerializeField] private CD_CityScriptableObject CDcityData;
        [SerializeField] private List<CD_Structure> CDstructureData;
        [SerializeField] private List<StructureData> structureData;

        #endregion

        #endregion

        private void Awake()
        {
            GetDatas();
            LoadDatas();
        }

        private void Start()
        {
            // SaveDatas();
        }

        #region Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            //CoreGameSignals.Instance.onSaveGameData += _saveGameCommand.OnSaveGameData;
            //CoreGameSignals.Instance.onLoadGameData += _loadGameCommand.OnLoadGameData;
        }

        private void UnSubscribe()
        {
          // CoreGameSignals.Instance.onSaveGameData -= _saveGameCommand.OnSaveGameData;
           // CoreGameSignals.Instance.onLoadGameData -= _loadGameCommand.OnLoadGameData;
        }

        #endregion]

        private void GetDatas()
        {
            CDcityData = Resources.Load<CD_CityScriptableObject>("Data/Idle/City");
            foreach(CD_Structure cityScriptableObject in CDcityData.CityScriptableObject)
            {
                CDstructureData.Add(cityScriptableObject);
            }

            foreach (var structure in CDstructureData)
            {
                structureData.Add(structure.StructureData);
            }
        }
        
        public void SaveDatas()
        {
            foreach (var structure in structureData)
            {
                ES3.Save(structure.BuildingType.ToString(),structure);
            }
        }

        public void LoadDatas()
        {
            var maxEnumCount = Enum.GetNames(typeof(BuildType)).Length;

            for (int i = 0; i < maxEnumCount; i++)
            {
                structureData[i] = ES3.Load(structureData[i].BuildingType.ToString(), structureData[i]);
            }
        }
    }
}