using System.Collections.Generic;
using CNC.Implementation.Controls;
using CNC.Implementation.Slots;
using CNC.Interfaces.Factories;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;
using Reflex.Attributes;
using Unity.VisualScripting;
using UnityEngine;

namespace CNC.Implementation.Factories
{
    public class SlotFactory<TControl, TContent, TTool> : ISlotFactory<TControl, TContent, TTool>
        where TControl : MonoBehaviour, ISlotControl<TContent, TTool>
        where TContent : MonoBehaviour, ISlotView<TTool>
        where TTool : IMainData
    {
        private TContent EdgePrefab { get; }
        private TControl ToolControlPrefab { get; }

        public SlotFactory(TContent edgePrefab, TControl toolControlPrefab)
        {
            EdgePrefab = edgePrefab;
            ToolControlPrefab = toolControlPrefab;
        }

        public ISlotControl<TContent, TTool> CreateSlotControl()
        {
            var toolControl = Object.Instantiate(ToolControlPrefab);
            return toolControl;
        }
        
        public SortedDictionary<int, TContent> CreateEdges(TTool tool)
        {
            var dict = new SortedDictionary<int, TContent>();
    
            foreach (var edgeIndex in tool.GetEdges)
            {
                var edge = Object.Instantiate(EdgePrefab);
                dict[edgeIndex] = edge;
            }
    
            return dict;
        }
        
        public TContent CreateSingleEdge()
        {
            return Object.Instantiate(EdgePrefab);
        }
    }
}