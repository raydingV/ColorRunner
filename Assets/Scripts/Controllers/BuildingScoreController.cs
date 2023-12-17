using System.Collections;
using Enums;
using Managers;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class BuildingScoreController : MonoBehaviour
    {

        [SerializeField] BuildingManager manager;
        private TextMeshPro _scoreText;
        private string _buildingName;
        private int _payedAmount;
        private int _price;

        private void Awake()
        {
            _scoreText = GetComponent<TextMeshPro>();
        }

        private void Start()
        {
            _scoreText.text = _buildingName + "\n" + _payedAmount + " / " + _price;
        }

        private void CheckPayAmount()
        {
            if(_payedAmount >= _price )
            {
                //buildin tipi complated yap 
            }
        }

        public void UpdatePayedAmount()
        {
            _payedAmount++;
            _scoreText.text = _buildingName + "\n" + _payedAmount + " / " + _price;
            CheckPayAmount();
        }

        public void GetData(string name, int payedAmount, int price)
        {
            _buildingName = name;
            _payedAmount = payedAmount;
            _price = price;
        }
    }
}