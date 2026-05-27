using CNC.Implementation.Controls;
using CNC.Implementation.Slots;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;

namespace CNC.Implementation.ToolPanel.Views
{
    public class ThirdToolPanelView : BaseToolPanelView<ThirdSlotControl, ThirdSlot, IMainData>
    {
        protected override void AddListeners(ISlotControl<ThirdSlot, IMainData> slotControl)
        {
            
        }

        protected override void RemoveListeners(ISlotControl<ThirdSlot, IMainData> slotControl)
        {
            
        }
    }
}