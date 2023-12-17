using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Keys;
using Signals;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region EventSubscribtion
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onRunnerSaveData += OnRunnerSaveData;
            SaveSignals.Instance.onIdleSaveData += OnIdleSaveData;
            SaveSignals.Instance.onSaveIdleParams += IdleSaveGame;
            SaveSignals.Instance.onLoadIdleGame += OnIdleGameLoad;
            SaveSignals.Instance.onRunnerGameLoad += OnRunnerGameLoad;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onRunnerSaveData -= OnRunnerSaveData;
            SaveSignals.Instance.onIdleSaveData -= OnIdleSaveData;
            SaveSignals.Instance.onSaveIdleParams -= IdleSaveGame;
            SaveSignals.Instance.onLoadIdleGame -= OnIdleGameLoad;
            SaveSignals.Instance.onRunnerGameLoad -= OnRunnerGameLoad;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        } 
        #endregion

        private void OnRunnerSaveData()
        {
            RunnerSaveGame(
                new SaveRunnerGameDataParams()
                {
                    Score = ScoreSignals.Instance.totalScore(),
                    Level = SaveSignals.Instance.onGetRunnerLevelID(),
                }
            );
        }

        private void RunnerSaveGame(SaveRunnerGameDataParams saveDataParams)
        {
            if (saveDataParams.Level != null) ES3.Save("Level", saveDataParams.Level, "RunnerGame.es3");
            if (saveDataParams.Score != null) ES3.Save("Score", saveDataParams.Score, "RunnerGame.es3");
        }

        private SaveRunnerGameDataParams OnRunnerGameLoad()
        {
            return new SaveRunnerGameDataParams()
            {
                Level = ES3.KeyExists("Level","RunnerGame.es3") ? ES3.Load<int>("Level","RunnerGame.es3") : 1,
                Score = ES3.KeyExists("Score","RunnerGame.es3") ? ES3.Load<int>("Score","RunnerGame.es3") : 1,
            };
        }
        
        private void OnIdleSaveData()
        {

        }
        
        private void IdleSaveGame(SaveIdleGameDataParams saveIdleDataParams)
        {
            if (saveIdleDataParams.IdleLevel != 0) ES3.Save("IdleLevel", saveIdleDataParams.IdleLevel,"IdleGame.es3");
            if (saveIdleDataParams.CollectablesCount != 0) ES3.Save("CollectablesCount", saveIdleDataParams.CollectablesCount,"IdleGame.es3");
            if (saveIdleDataParams.MainPayedAmount != null) ES3.Save("MainCurrentScore", saveIdleDataParams.MainPayedAmount,"IdleGame.es3");
            if (saveIdleDataParams.SidePayedAmount != null) ES3.Save("SideCurrentScore", saveIdleDataParams.SidePayedAmount,"IdleGame.es3");
            if (saveIdleDataParams.MainBuildingState != null) ES3.Save("MainBuildingState", saveIdleDataParams.MainBuildingState,"IdleGame.es3");
            if (saveIdleDataParams.SideBuildingState != null) ES3.Save("SideBuildingState", saveIdleDataParams.SideBuildingState,"IdleGame.es3");
        }

        
        private SaveIdleGameDataParams OnIdleGameLoad()
        {
            return new SaveIdleGameDataParams()
                {
                    IdleLevel = ES3.KeyExists("IdleGame","Idlegame.es3")? ES3.Load<int>("IdleGame","IdleGame.es3"):0,
                    CollectablesCount = ES3.KeyExists("CollectablesCount","Idlegame.es3")? ES3.Load<int>("CollectablesCount","IdleGame.es3"):0,
                    MainPayedAmount = ES3.KeyExists("MainCurrentScore","Idlegame.es3")? ES3.Load<List<int>>("MainCurrentScore","IdleGame.es3"):default,
                    SidePayedAmount = ES3.KeyExists("SideCurrentScore","Idlegame.es3")? ES3.Load<List<int>>("SideCurrentScore","IdleGame.es3"):default,
                    MainBuildingState = ES3.KeyExists("MainBuildingState","Idlegame.es3")? ES3.Load<List<BuildingComplateState>>("MainBuildingState","IdleGame.es3"):default,
                    SideBuildingState = ES3.KeyExists("SideBuildingState","Idlegame.es3")? ES3.Load<List<BuildingComplateState>>("SideBuildingState","IdleGame.es3"):default,
                };
        }
    }
}