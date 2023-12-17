using System;
using System.Threading.Tasks;
using Controllers;
using Data.ValueObject;
using Enums;
using Signals;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    public class BuildingManager : MonoBehaviour
    {
        #region Variables

        #region Public

        public BuildingData BuildingData;
        
        #endregion

        #region Serialized

        [SerializeField] private GameObject mainBuilding;
        [SerializeField] private TextMeshPro mainText;
        [SerializeField] private BuildingMeshController mainMesh;
        
        [SerializeField] private GameObject sideBuilding;
        [SerializeField] private TextMeshPro sideText;
        [SerializeField] private BuildingMeshController sideMesh;
        
        #endregion

        #region Private
        
        private string _mainBuildingName;
        private BuildingComplateState _mainBuildingComplateState;
        private int _mainPrice;
        private int _mainPayedAmount;

        private string _sideBuildingName;
        private BuildingComplateState _sideBuildingComplateState;
        private int _sidePrice;
        private int _sidePayedAmount;
        
        #endregion

        #endregion

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onDataGet += Init;
        }

        private void UnSubscribeEvents()
        {
            SaveSignals.Instance.onDataGet -= Init;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        private void Init()
        {
            SetDatas(BuildingData);
            CheckDatas();
            InitializeTextOnStart();
        }
        
        public void SetDatas(BuildingData buildingData)
        {
            _mainBuildingName = buildingData.mainBuildingData.BuildingName;
            _mainBuildingComplateState = buildingData.mainBuildingData.CompleteState;
            _mainPrice = buildingData.mainBuildingData.Price;
            _mainPayedAmount = buildingData.mainBuildingData.PayedAmount;
            
            _sideBuildingName = buildingData.sideBuildindData.BuildingName;
            _sideBuildingComplateState = buildingData.sideBuildindData.CompleteState;
            _sidePrice = buildingData.sideBuildindData.Price;
            _sidePayedAmount = buildingData.sideBuildindData.PayedAmount;
        }

        private void CheckDatas()
        {
            CheckComplateState(true, _mainBuildingComplateState, _mainPayedAmount, _mainPrice);
            CheckComplateState(false, _sideBuildingComplateState, _sidePayedAmount, _sidePrice);
        }
        
        public void OnPlayerEnter()
        {
            if (_mainBuildingComplateState == BuildingComplateState.Uncompleted && _sideBuildingComplateState == BuildingComplateState.Uncompleted)
            {
                if (ScoreSignals.Instance.totalScore() > 0)
                {
                    ScoreSignals.Instance.onTotalScoreUpdate?.Invoke(-1);
                    ScoreSignals.Instance.onUpdateScoreText?.Invoke();
                    CheckComplateState(true, _mainBuildingComplateState, _mainPayedAmount, _mainPrice);
                    PlayerSignals.Instance.onScaleDown?.Invoke();
                    _mainPayedAmount++;
                    SetText(mainText,_mainBuildingName, _mainPayedAmount, _mainPrice);
                    SetDataToBuildingData();
                }
            }
            else if (_mainBuildingComplateState == BuildingComplateState.Completed && _sideBuildingComplateState == BuildingComplateState.Uncompleted)
            {
                if (ScoreSignals.Instance.totalScore() > 0)
                {
                    ScoreSignals.Instance.onTotalScoreUpdate?.Invoke(-1);
                    ScoreSignals.Instance.onUpdateScoreText?.Invoke();
                    PlayerSignals.Instance.onScaleDown?.Invoke();
                    CheckComplateState(false,_sideBuildingComplateState,_sidePayedAmount,_sidePrice);
                    _sidePayedAmount++;
                    SetText(sideText,_sideBuildingName,_sidePayedAmount,_sidePrice);
                    SetDataToBuildingData();
                }
            }
        }

        private void CheckComplateState(bool isMain,BuildingComplateState complateState, int payedAmount, int price)
        {
            if (isMain && payedAmount >= price)
            {
                _mainBuildingComplateState = BuildingComplateState.Completed;
                mainBuilding.SetActive(false);
                sideBuilding.SetActive(true);
                mainMesh.ChangeBuildingSaturation(1.5f);
            }

            else if (!isMain && payedAmount >= price)
            {
                _sideBuildingComplateState = BuildingComplateState.Completed;
                sideBuilding.SetActive(false);
                sideMesh.ChangeBuildingSaturation(1.5f);
            }
        }

        public void SetText(TextMeshPro text,string BuildingName ,int PayedAmount, int price)
        {
            text.text = BuildingName + "\n" + PayedAmount + " / " + price;
        }

        private void InitializeTextOnStart()
        {
            SetText(mainText,_mainBuildingName,_mainPayedAmount,_mainPrice);
            SetText(sideText,_sideBuildingName,_sidePayedAmount,_sidePrice);
        }

        private void SetDataToBuildingData()
        {
            BuildingData.mainBuildingData.PayedAmount = _mainPayedAmount;
            BuildingData.mainBuildingData.CompleteState = _mainBuildingComplateState;
            
            BuildingData.sideBuildindData.PayedAmount = _sidePayedAmount;
            BuildingData.sideBuildindData.CompleteState = _sideBuildingComplateState;
        }
    }
}