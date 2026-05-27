using CNC.Implementation.Controls;
using CNC.Implementation.Slots;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;

namespace CNC.Implementation.ToolPanel.Views
{
    public class TurningOffsetPanelView : BaseToolPanelView<TurningOffsetSlotControl, TurningOffsetSlot, ITurningTool>
    {
        protected override void AddListeners(ISlotControl<TurningOffsetSlot, ITurningTool> slotControl)
        {
            
        }

        protected override void RemoveListeners(ISlotControl<TurningOffsetSlot, ITurningTool> slotControl)
        {
            
        }
    }
}