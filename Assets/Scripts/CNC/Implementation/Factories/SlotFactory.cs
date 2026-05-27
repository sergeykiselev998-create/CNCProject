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

        public List<TContent> CreateEdges(TTool tool)
        {
            var list = new List<TContent>();
            for (var i = 0; i < tool.CountEdges; i++)
            {
                var content = Object.Instantiate(EdgePrefab);
                list.Add(content);
            }

            return list;
        }
    }
}