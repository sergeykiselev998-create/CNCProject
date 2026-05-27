using CNC.Implementation.Controls;
using CNC.Implementation.Slots;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;

namespace CNC.Implementation.ToolPanel.Views
{
    public class FourthToolPanelView : BaseToolPanelView<FourthSlotControl, FourthSlot, IMainData>
    {
        protected override void AddListeners(ISlotControl<FourthSlot, IMainData> slotControl)
        {
            
        }

        protected override void RemoveListeners(ISlotControl<FourthSlot, IMainData> slotControl)
        {

        }
    }
}