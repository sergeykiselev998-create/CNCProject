using System;
using System.Collections.Generic;
using System.Linq;
using CNC.Implementation.Controls;
using CNC.Implementation.Factories;
using CNC.Enums;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolPanel;
using CNC.Interfaces.Views;
using Reflex.Attributes;
using UnityEngine;

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
        private List<ISlotControl<TContent, TTool>> Buffer { get; } = new();
        
        [Inject]
        public SlotFactory<TControl, TContent, TTool> Factory { get; set; }

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
            
            Buffer.Add(toolControl);
        }
        
        protected bool TryFindControlById(int id, out ISlotControl<TContent, TTool> control)
        {
            if (Spindle != null && Spindle.Id == id)
            {
                control = Spindle;
                return true;
            }

            if (Magazine.TryGetValue(id, out control))
                return true;

            control = Buffer.FirstOrDefault(c => c.Id == id);
            
            
            return control != null;
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

        protected abstract void AddListeners(ISlotControl<TContent, TTool> slotControl);

        protected abstract void RemoveListeners(ISlotControl<TContent, TTool> slotControl);
    }
}