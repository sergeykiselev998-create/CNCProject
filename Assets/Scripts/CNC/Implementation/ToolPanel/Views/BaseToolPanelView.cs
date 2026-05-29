using System.Collections.Generic;
using CNC.Implementation.Factories;
using CNC.Enums;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolPanel;
using CNC.Interfaces.Views;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace CNC.Implementation.ToolPanel.Views
{
    public abstract class BaseToolPanelView<TControl, TContent, TTool>: MonoBehaviour, IToolPanelView<TControl,TContent, TTool>
    where TControl : MonoBehaviour, ISlotControl<TContent, TTool>
    where TContent : MonoBehaviour, ISlotView<TTool>
    where TTool : IMainData
    {
        [Header("Containers")]
        [SerializeField] protected Transform m_SpindleContainer;
        [SerializeField] protected Transform m_MagazineContainer;
        [SerializeField] protected Transform m_BufferContainer;

        private ISlotControl<TContent, TTool> Spindle { get; set; }
        private Dictionary<int, ISlotControl<TContent, TTool>> Magazine { get; } = new();
        private Dictionary<int, ISlotControl<TContent, TTool>>  Buffer { get; } = new();
        
        [Inject]
        public SlotFactory<TControl, TContent, TTool> Factory { get; set; }

        //________Add_______//

        public void AddToolHolder(int location, TTool tool)
        {
            var toolControl = Create(location, tool, m_SpindleContainer.transform, SlotLocationType.Spindle);
            Spindle = toolControl;
        }

        public void AddMagazine(int location, TTool tool)
        {
            var toolControl = Create(location, tool, m_MagazineContainer.transform, SlotLocationType.Magazine);
            Magazine[location] = toolControl;
        }

        public void AddBuffer(int location, TTool tool)
        {
            var toolControl = Create(location, tool, m_BufferContainer.transform, SlotLocationType.Buffer);
            Buffer.Add(tool.Id, toolControl);
        }

        //________Unload_______//

        public void UnloadToolHolder(int location, TTool emptyTool)
        {
            if (Spindle == null)
                return;
            
            ReinitializeControl(Spindle, location, emptyTool, SlotLocationType.Spindle);
        }
        
        public void UnloadMagazine(int location, TTool emptyTool)
        {
            if (!Magazine.TryGetValue(location, out var slotControl))
                return;
            
            ReinitializeControl(slotControl, location, emptyTool, SlotLocationType.Magazine);
        }

        public void UnloadBuffer(int toolId)
        {
            if (!Buffer.TryGetValue(toolId, out var slotControl))
                return;

            RemoveListeners(slotControl);
            slotControl.Destroy();
            Buffer.Remove(toolId);
        }

        //________Load_______//
        
        public void LoadToolHolder(int location, TTool tool)
        {
            if (Spindle == null)
                return;
            
            ReinitializeControl(Spindle, location, tool, SlotLocationType.Spindle);
        }

        public void LoadMagazine(int location, TTool tool)
        {
            if (!Magazine.TryGetValue(location, out var slotControl))
                return;
            
            ReinitializeControl(slotControl, location, tool, SlotLocationType.Magazine);
        }

        public void LoadBuffer(int location, TTool tool)
        {
            if (Buffer.ContainsKey(tool.Id))
                return;
            
            var toolControl = Create(location, tool, m_BufferContainer.transform, SlotLocationType.Buffer);
            Buffer.Add(tool.Id, toolControl);
        }
        
        //AddEdge
        public void AddEdge(int id)
        {
            if (!TryFindControlById(id, out var control))
                return;
            
                    // control.AddEdge();
        }
        

        //________Protected_______//
        
        protected bool TryFindControlById(int id, out ISlotControl<TContent, TTool> control)
        {
            if (Spindle != null && Spindle.Id == id)
            {
                control = Spindle;
                return true;
            }

            if (Magazine.TryGetValue(id, out control))
                return true;

            if (Buffer.TryGetValue(id, out control)) 
                return true;
            
            control = null;
            return false;
        }

        protected virtual bool TryCastControl(ISlotControl<TContent, TTool> slotControl, out TControl control)
        {
            control = slotControl as TControl;

            if (control == null)
            {
                Debug.Log($"[{GetType().Name}] slotControl is not {typeof(TControl).Name}");
                return false;
            }

            return true;
        }
        
        //________Private_______//
        
        private void ReinitializeControl(ISlotControl<TContent, TTool> control, int location, TTool tool, SlotLocationType locationType)
        {
            control.RemoveEdgesAndReset();
    
            var newEdges = Factory.CreateEdges(tool);
    
            control.Initialize(location, tool, newEdges);
            control.UpdateVisual(locationType);
        }
        
        private ISlotControl<TContent, TTool> Create(int location, TTool tool, Transform parent, SlotLocationType locationType)
        {
            var toolControl =  Factory.CreateSlotControl();
            var toolEdges = Factory.CreateEdges(tool);
            
            toolControl.SetParent(parent);
            toolControl.Initialize(location, tool, toolEdges);
            toolControl.UpdateVisual(locationType);
            AddListeners(toolControl);
            return toolControl;
        }

        //________Abstract_______//
        
        protected abstract void AddListeners(ISlotControl<TContent, TTool> slotControl);

        protected abstract void RemoveListeners(ISlotControl<TContent, TTool> slotControl);
    }
}