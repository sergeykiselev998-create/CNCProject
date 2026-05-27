using CNC.Enums;
using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Views
{
    public interface ISlotView<in TTool>
    where TTool : IToolData
    {
        void ApplyState(SlotDisplayType state, SlotLocationType context);
        void UpdateData(int location, int edge, TTool tool);
        void SetSiblingIndex(int siblingIndex);
        void ResetUI();
    }
}


