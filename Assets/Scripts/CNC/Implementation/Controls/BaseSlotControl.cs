using System.Collections.Generic;
using CNC.Enums;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;
using UnityEngine;

namespace CNC.Implementation.Controls
{
    public abstract class BaseSlotControl<TContent, TTool> : MonoBehaviour, ISlotControl<TContent, TTool>
        where TContent : MonoBehaviour, ISlotView<TTool>
        where TTool : ITool
    {
        private TTool Tool;
        private List<TContent> ToolEdges;
        
        public void Initialize(int location, TTool tool, List<TContent> toolEdges)
        {
            Tool = tool;
            ToolEdges = toolEdges;
 
            if (ToolEdges.Count != Tool.Offsets.Count)
            {
                Debug.Log("[ToolControl] ToolEdges.Count != Tool.Offsets.Count ");
                return;
            }
            
            int index = 0;
            foreach (var toolOffset in Tool.Offsets)
            {
                var edge = ToolEdges[index];
                edge.transform.SetParent(transform);
                edge.UpdateData(location, toolOffset.Key, tool);
                index++;
            }
        }


        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void UpdateVisual(SlotLocationType slotLocationType)
        {
            int index = 0;
            foreach (var toolOffset in Tool.Offsets)
            {
                var edge = ToolEdges[index];

                var siplayType = Tool.Id <= 0 
                    ? SlotDisplayType.Unload 
                    : (index == 0 
                        ? SlotDisplayType.Load 
                        : SlotDisplayType.Edge);

                edge.ApplyState(siplayType, slotLocationType);


                index++;
            }
        }
    }
}