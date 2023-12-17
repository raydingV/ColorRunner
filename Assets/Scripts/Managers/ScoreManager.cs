using System;
using Keys;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Variables
        
        #region Serialize

        [SerializeField] private float localScaleMultiplier;
        [SerializeField] private GameObject backgroundImage;
        [SerializeField] private Vector3 followRunnerOffset;
        [SerializeField] private Vector3 followIdleOffset;
        
        #endregion

        #region Private

        private Vector3 followOffset;
        private int _currentScore;
        private int _totalScore;
        private Transform _target;
        private TextMeshPro _scoreText;
        
        #endregion
        
        #endregion

        private void Awake()
        {
            _scoreText = GetComponent<TextMeshPro>();
            OnHideScore();
        }

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onCurrentLevelScoreUpdate += OnCurrentLevelScoreUpdate;
            ScoreSignals.Instance.onTotalScoreUpdate += OnTotalScoreUpdate;
            ScoreSignals.Instance.currentScore += ReturnCurrentScore;
            ScoreSignals.Instance.totalScore += ReturnTotalScore;
            ScoreSignals.Instance.onHideScore += OnHideScore;
            ScoreSignals.Instance.onShowScoreIdle += OnShowScore;
            ScoreSignals.Instance.onUpdateScoreAfterDroneArea += OnUpdateScoreAfterDroneArea;
            StackSignals.Instance.onSetScoreControllerPosition += OnSetPosition;
            PlayerSignals.Instance.onPlayerEnterIdleArea += OnPlayerEnterIdleArea;
            
            CoreGameSignals.Instance.onPlay += OnFindFollowTarget;
            CoreGameSignals.Instance.onReset += OnReset;
            
            LevelSignals.Instance.onNextLevel += OnNextLevel;
        }
        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onCurrentLevelScoreUpdate -= OnCurrentLevelScoreUpdate;
            ScoreSignals.Instance.onTotalScoreUpdate -= OnTotalScoreUpdate;
            ScoreSignals.Instance.currentScore -= ReturnCurrentScore;
            ScoreSignals.Instance.totalScore -= ReturnTotalScore;
            ScoreSignals.Instance.onHideScore -= OnHideScore;
            ScoreSignals.Instance.onShowScoreIdle -= OnShowScore;
            ScoreSignals.Instance.onUpdateScoreAfterDroneArea -= OnUpdateScoreAfterDroneArea;
            StackSignals.Instance.onSetScoreControllerPosition -= OnSetPosition;
            PlayerSignals.Instance.onPlayerEnterIdleArea -= OnPlayerEnterIdleArea;
            
            CoreGameSignals.Instance.onPlay -= OnFindFollowTarget;
            CoreGameSignals.Instance.onReset -= OnReset;

            LevelSignals.Instance.onNextLevel -= OnNextLevel;
        }

        #endregion

        private void Start()
        {
            _totalScore = SaveSignals.Instance.onRunnerGameLoad().Score;
            backgroundImage.SetActive(false);
            _scoreText.text = "";
        }

        private void Update()
        {
            if (_target == null) return;
            transform.position = new Vector3(_target.position.x,
                _target.position.y + followOffset.y + 
                (_target.transform.localScale.y - 1) * (_target.localScale.y - 1) * localScaleMultiplier,
                _target.position.z + followOffset.z);
        }

        private void OnFindFollowTarget()
        {
            _target = StackSignals.Instance.onGetFirstCollectable();
            followOffset = followRunnerOffset;
            _scoreText.text = _currentScore.ToString();
            backgroundImage.SetActive(true);
        }

        private void OnCurrentLevelScoreUpdate(bool increase)
        {
            _currentScore += (increase) ? 1 : -1;
            _scoreText.text = _currentScore.ToString();
            SaveScoreParams();
        }

        private void OnTotalScoreUpdate(int score)
        {
            _totalScore += score;
            _scoreText.text = _totalScore.ToString();
            SaveScoreParams();
        }

        private void OnSetPosition(Transform _transform)
        {
            _target = _transform;
        }

        private void OnPlayerEnterIdleArea()
        {
            _target = FindObjectOfType<PlayerManager>().transform;
            followOffset = followIdleOffset;
            backgroundImage.SetActive(false);
            _scoreText.text = "";
        }
            
        public void OnUpdateScoreAfterDroneArea()
        {
            backgroundImage.SetActive(true);
            _scoreText.text = _currentScore.ToString();
        }

        private void OnHideScore()
        {
            backgroundImage.SetActive(false);
            _scoreText.text = "";
        }

        private void OnShowScore()
        {
            backgroundImage.SetActive(true);
            _scoreText.enabled = true;
            _currentScore = 0;
            _scoreText.text = _totalScore.ToString();
        }
        
        private void OnReset()
        {
            backgroundImage.SetActive(false);
            _scoreText.text = "";
            _currentScore = 0;
            followOffset = followRunnerOffset;
        }
        
        #region refactred when saveManager is added
        private void SaveScoreParams()
        {
            new ScoreParams() {currentLevelScore = _currentScore, totalScore = _totalScore };
        }

        private void OnNextLevel()
        {
            SaveSignals.Instance.onRunnerSaveData?.Invoke();
        }
        
        private int ReturnCurrentScore() {return _currentScore; }
        
        private int ReturnTotalScore() {return _totalScore; }
        
        #endregion
    }
}