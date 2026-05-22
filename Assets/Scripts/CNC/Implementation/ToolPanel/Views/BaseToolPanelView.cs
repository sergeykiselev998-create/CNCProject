using System;
using System.Collections.Generic;
using CNC.Implementation.Controls;
using CNC.Implementation.Factories;
using CNC.Implementation.Slots;
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
    where TTool : ITool
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
            var toolControl = Create(location, tool, m_SpindleContainer.transform);
            Spindle = toolControl;
        }

        public void AddMagazine(int location, TTool tool)
        {
            var toolControl = Create(location, tool, m_MagazineContainer.transform);
            Magazine[location] = toolControl;
        }

        public void AddBuffer(int location, TTool tool)
        {
            var toolControl = Create(location, tool, m_BufferContainer.transform);
            
            Buffer.Add(toolControl);
        }
        

        private ISlotControl<TContent, TTool> Create(int location, TTool tool, Transform parent)
        {
            var toolControl =  Factory.CreateSlotControl();
            var toolEdges = Factory.CreateEdges(tool);
            
            toolControl.SetParent(parent);
            toolControl.Initialize(location, tool, toolEdges);
            
            return toolControl;
        }
    }
}