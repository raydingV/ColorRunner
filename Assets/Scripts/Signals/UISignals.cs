using Enums;
using Extentions;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIPanels> onOpenPanel;
        public UnityAction<UIPanels> onClosePanel;
        public UnityAction<int> onUpdateStageData;
        public UnityAction<int> onSetLevelText;
        public UnityAction<int> onIdleMoneyMultiplier;
    }
}