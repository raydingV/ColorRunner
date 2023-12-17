using Enums;
using Signals;

namespace Commands
{
    public class ChangeAllCollectableColorTypeCommand
    {
        public void OnChangeAllCollectableColorType(ColorType type)
        {
            StackSignals.Instance.onChangeMatarialColor?.Invoke(type);
        }
    }
}