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
        protected SortedDictionary<int, TContent>  ToolEdges;
        public int Id => Tool.Id;
        
        private SlotLocationType CurrentLocationType { get; set; }
        private int CurrentLocation { get; set; }
        
        protected abstract void AddListeners();
        protected abstract void RemoveListeners();
        
        public void Initialize(int location, TTool tool, SortedDictionary<int, TContent>  toolEdges)
        {
            Tool = tool;
            ToolEdges = toolEdges;
            CurrentLocation = location;
 
            if (ToolEdges.Count != Tool.CountEdges)
            {
                Debug.Log("[ToolControl] ToolEdges.Count != Tool.Offsets.Count ");
                return;
            }

            foreach (var edgeIndex in tool.GetEdges)
            {
                var edge = ToolEdges[edgeIndex];
                edge.transform.SetParent(transform);
                edge.UpdateData(location, edgeIndex, tool);
            }

            AddListeners();
        }

        public void UpdateVisual(SlotLocationType slotLocationType)
        {
            CurrentLocationType = slotLocationType;
            
            var i = 0;
            foreach (var edge in ToolEdges.Values)
            {
                edge.SetSiblingIndex(i);
        
                var displayType = Tool.Id <= 0 
                    ? SlotDisplayType.Unload 
                    : (i == 0 
                        ? SlotDisplayType.Load 
                        : SlotDisplayType.Edge);

                edge.ApplyState(displayType, slotLocationType);
                i++;
            }
        }
        
        public void AddEdge(int edgeIndex, TContent edge)
        {
            ToolEdges[edgeIndex] = edge;
    
            edge.transform.SetParent(transform);
            edge.UpdateData(CurrentLocation, edgeIndex, Tool);
            edge.SetSiblingIndex(edgeIndex - 1);
    
            UpdateVisual(CurrentLocationType);
        }

        public void RemoveEdge(int edgeIndex)
        {
            if (edgeIndex <= 1) return;
    
            if (ToolEdges.Remove(edgeIndex, out var edge))
            {
                Destroy(edge.gameObject);
            }
    
            UpdateVisual(CurrentLocationType);
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }
        
        protected bool TryGetEdge(int edgeIndex, out TContent edge)
        {
            var result = ToolEdges.TryGetValue(edgeIndex, out edge);
            
            if(!result)
                Debug.Log($"[{this.GetType().Name}] Edge not found with edgeIndex: {edgeIndex}");
            
            return result;
        }

        public void RemoveEdgesAndReset()
        {
            Dispose();
            Tool = default;
            foreach (var edge in ToolEdges.Values)
            {
                Destroy(edge);
            }
            ToolEdges.Clear();
        }
        
        public void Destroy()
        {
            RemoveEdgesAndReset();
            Destroy(gameObject);
        }

        private void Dispose()
        {
            RemoveListeners();
        }
        
        private void OnDestroy()
        {
            Dispose();
        }
    }
}