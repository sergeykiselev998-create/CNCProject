using System.Linq;
using CNC.Implementation.Slots;
using CNC.Interfaces.Tool;
using UnityEngine;
using UnityEngine.Events;

namespace CNC.Implementation.Controls
{
    public class MainSlotControl : BaseSlotControl<MainSlot,IMainData>
    {
        public event UnityAction<int, string> OnNameChanged;
        public event UnityAction<int> OnNameChangeFailed;
        public event UnityAction<int, int> OnAddEdge;
        public event UnityAction<int, int> OnRemoveEdge;
        
        protected override void AddListeners()
        {
            AddNameListener();
            AddEdgeListeners();
        }
        
        protected override void RemoveListeners()
        {
            RemoveToolNameListener();
            RemoveEdgeListeners();
        }

        private void AddNameListener()
        {
            if (!TryGetFirstEdge(out var mainEdge))
                return;
            
            mainEdge.OnNameChanged += ValidateName;
        }
        
        private void RemoveToolNameListener()
        {
            if (!TryGetFirstEdge(out var mainEdge))
                return;
            
            mainEdge.OnNameChanged -= ValidateName;
        }

        private void AddEdgeListeners()
        {
            foreach (var mainEdge in ToolEdges.Values)
            {
                mainEdge.OnAddEdge += HandleAddEdge;
                mainEdge.OnRemoveEdge += HandleRemoveEdge;
            }
        }
        
        private void RemoveEdgeListeners()
        {
            foreach (var mainEdge in ToolEdges.Values)
            {
                mainEdge.OnAddEdge -= HandleAddEdge;
                mainEdge.OnRemoveEdge -= HandleRemoveEdge;
            }
        }

        private bool TryGetFirstEdge(out MainSlot mainEdge)
        {
            var result = ToolEdges.TryGetValue(1, out mainEdge);
            
            if (!result)
                Debug.Log("[MainSlotControl] ToolEdges is empty");
            
            return result;
        }
        
        private void ValidateName(string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                OnNameChangeFailed?.Invoke(Tool.Id);
                return;
            }
            
            OnNameChanged?.Invoke(Tool.Id, newName);
        }

        public void SetNameWithoutNotify(string newName)
        {
            foreach (var edge in ToolEdges.Values)
            {
                edge.SetNameWithoutNotify(newName);
            }
        }

        private void HandleAddEdge(int triggeredEdgeIndex)
        {
            OnAddEdge?.Invoke(Tool.Id, triggeredEdgeIndex);
        }
        
        private void HandleRemoveEdge(int edgeIndex)
        {
            OnRemoveEdge?.Invoke(Tool.Id, edgeIndex);
        }
    }
}