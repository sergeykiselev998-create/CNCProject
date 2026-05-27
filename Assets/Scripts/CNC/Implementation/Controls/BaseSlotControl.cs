using System.Collections.Generic;
using CNC.Enums;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;
using UnityEngine;

namespace CNC.Implementation.Controls
{
    public abstract class BaseSlotControl<TContent, TTool> : MonoBehaviour, ISlotControl<TContent, TTool>
        where TContent : MonoBehaviour, ISlotView<TTool>
        where TTool : IMainData
    {
        protected TTool Tool;
        protected List<TContent> ToolEdges;
        public int Id => Tool.Id;
        
        public void Initialize(int location, TTool tool, List<TContent> toolEdges)
        {
            Tool = tool;
            ToolEdges = toolEdges;
 
            if (ToolEdges.Count != Tool.CountEdges)
            {
                Debug.Log("[ToolControl] ToolEdges.Count != Tool.Offsets.Count ");
                return;
            }

            var i = 0;
            foreach (var edgeIndex in tool.GetEdges)
            {
                var edge = ToolEdges[i];
                edge.transform.SetParent(transform);
                edge.UpdateData(location, edgeIndex, tool);
                i++;
            }

            AddListeners();
        }

        protected abstract void AddListeners();
        protected abstract void RemoveListeners();


        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void UpdateVisual(SlotLocationType slotLocationType)
        {
            for (var i = 0; i < ToolEdges.Count; i++)
            {
                var edge = ToolEdges[i];

                var displayType = Tool.Id <= 0 
                    ? SlotDisplayType.Unload 
                    : (i == 0 
                        ? SlotDisplayType.Load 
                        : SlotDisplayType.Edge);

                edge.ApplyState(displayType, slotLocationType);
            }
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }
    }
}