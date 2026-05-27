using CNC.Implementation.Controls;
using CNC.Implementation.Slots;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;
using UnityEngine.Events;

namespace CNC.Implementation.ToolPanel.Views
{
    public class MillingOffsetPanelView : BaseToolPanelView<MillingOffsetSlotControl, MillingOffsetSlot, IMillingTool>
    {
        public event UnityAction<int, int, float> OnLengthChanged;
        public event UnityAction<int, int> OnLengthChangeFailed;
        
        public event UnityAction<int, int, float> OnDiameterChanged;
        public event UnityAction<int, int> OnDiameterChangeFailed;
        protected override void AddListeners(ISlotControl<MillingOffsetSlot, IMillingTool> slotControl)
        {
            if (!TryCastControl(slotControl, out var typedControl))
                return;
            
            typedControl.OnLengthChanged += OnLengthChanged;
            typedControl.OnLengthChangeFailed += OnLengthChangeFailed;
            
            typedControl.OnDiameterChanged += OnDiameterChanged;
            typedControl.OnDiameterChangeFailed += OnDiameterChangeFailed;
        }

        protected override void RemoveListeners(ISlotControl<MillingOffsetSlot, IMillingTool> slotControl)
        {
            if (!TryCastControl(slotControl, out var typedControl))
                return;
            
            typedControl.OnLengthChanged -= OnLengthChanged;
            typedControl.OnLengthChangeFailed -= OnLengthChangeFailed;
            
            typedControl.OnDiameterChanged -= OnDiameterChanged;
            typedControl.OnDiameterChangeFailed -= OnDiameterChangeFailed;
        }
        
        public void UpdateLengthInControl(int id, int edgeIndex, float length)
        {
            if (!TryFindControlById(id, out var slotControl))
                return;
            
            if (!TryCastControl(slotControl, out var typedControl))
                return;
            
            typedControl.SetLengthWithoutNotify(edgeIndex, length);
        }
        
        public void UpdateDiameterInControl(int id, int edgeIndex, float length)
        {
            if (!TryFindControlById(id, out var slotControl))
                return;
            
            if (!TryCastControl(slotControl, out var typedControl))
                return;
            
            typedControl.SetDiameterWithoutNotify(edgeIndex, length);
        }
    }
}