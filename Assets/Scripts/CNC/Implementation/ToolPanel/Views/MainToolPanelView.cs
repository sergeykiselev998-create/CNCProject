using System;
using CNC.Implementation.Controls;
using CNC.Implementation.Slots;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;
using UnityEngine;
using UnityEngine.Events;

namespace CNC.Implementation.ToolPanel.Views
{
    public class MainToolPanelView : BaseToolPanelView<MainSlotControl, MainSlot, IMainData>
    {
        public event UnityAction<int, string> OnNameChanged;
        public event UnityAction<int> OnNameChangeFailed;
        
        protected override void AddListeners(ISlotControl<MainSlot, IMainData> slotControl)
        {
            if (!TryCastControl(slotControl, out var typedControl))
                return;
            
            typedControl.OnNameChanged += OnNameChanged;
            typedControl.OnNameChangeFailed += OnNameChangeFailed;
        }
        
        protected override void RemoveListeners(ISlotControl<MainSlot, IMainData> slotControl)
        {
            if (!TryCastControl(slotControl, out var typedControl))
                return;
            
            typedControl.OnNameChanged -= OnNameChanged;
            typedControl.OnNameChangeFailed -= OnNameChangeFailed;
        }

        public void UpdateToolNameInControl(int id, string formatName)
        {
            if (!TryFindControlById(id, out var slotControl))
                return;
            
            if (!TryCastControl(slotControl, out var typedControl))
                return;
            
            typedControl.SetNameWithoutNotify(formatName);
        }
    }
}