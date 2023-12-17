using DG.Tweening;
using Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Commands
{
    public class PrizeArrowMoveCommand
    {
        private Image _arrow;
        private int _moveAmount;
        private float _duration;

        public PrizeArrowMoveCommand(ref Image arrow, ref int moveAmount, ref float duration)
        {
            _arrow = arrow;
            _moveAmount = moveAmount;
            _duration = duration;
        }
        
        public void Execute()
        {
            MoveRight();
        }

        private void MoveRight()
        {
            CheckPosition();
            _arrow.transform.DOLocalMoveX(_moveAmount, _duration).SetRelative(true).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    CheckPosition();
                    if(_arrow.rectTransform.anchoredPosition.x < 1550)
                        MoveRight();
                    else
                        MoveLeft();
                });
        }
        
        private void MoveLeft()
        {
            
            CheckPosition();
            _arrow.transform.DOLocalMoveX(-_moveAmount, _duration).SetRelative(true).SetEase(Ease.Linear).OnComplete(() =>
            {
                CheckPosition();
                if(_arrow.rectTransform.anchoredPosition.x < 400)
                    MoveRight();
                else
                    MoveLeft();
            });
        }

        private void CheckPosition()
        {
            if (_arrow.rectTransform.anchoredPosition.x >= 350 && _arrow.rectTransform.anchoredPosition.x < 550)
            {
                UISignals.Instance.onIdleMoneyMultiplier?.Invoke(2);
            }
            else if (_arrow.rectTransform.anchoredPosition.x >= 550 && _arrow.rectTransform.anchoredPosition.x < 800)
            {
                UISignals.Instance.onIdleMoneyMultiplier?.Invoke(3);
            }
            else if (_arrow.rectTransform.anchoredPosition.x >= 800 && _arrow.rectTransform.anchoredPosition.x < 1100)
            {
                UISignals.Instance.onIdleMoneyMultiplier?.Invoke(5);
            }
            else if (_arrow.rectTransform.anchoredPosition.x >= 1100 && _arrow.rectTransform.anchoredPosition.x < 1350)
            {
                UISignals.Instance.onIdleMoneyMultiplier?.Invoke(3);
            }
            else if (_arrow.rectTransform.anchoredPosition.x >= 1350 && _arrow.rectTransform.anchoredPosition.x < 1600)
            {
                UISignals.Instance.onIdleMoneyMultiplier?.Invoke(2);
            }
        }
    }
}