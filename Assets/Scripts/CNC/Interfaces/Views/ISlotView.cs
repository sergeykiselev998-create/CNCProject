using CNC.Enums;
using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Views
{
    public interface ISlotView<in TTool>
    where TTool : ITool
    {
        void ApplyState(SlotState state, SlotContext context);
        void UpdateData(int location, int edge, TTool tool);
        void SetSiblingIndex(int siblingIndex);
        void ResetUI();
    }
}


