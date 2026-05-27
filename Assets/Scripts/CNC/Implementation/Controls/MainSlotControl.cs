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
        
        protected override void AddListeners()
        {
            AddNameListener();
        }
        
        protected override void RemoveListeners()
        {
            RemoveNameListener();
        }

        private void AddNameListener()
        {
            if (!TryGetFirstEdge(out var mainEdge))
                return;
            
            mainEdge.OnNameChanged += ValidateName;
        }
        
        private void RemoveNameListener()
        {
            if (!TryGetFirstEdge(out var mainEdge))
                return;
            
            mainEdge.OnNameChanged -= ValidateName;
        }

        private bool TryGetFirstEdge(out MainSlot mainEdge)
        {
            mainEdge = ToolEdges.FirstOrDefault();
            var result = mainEdge == null;
            
            if (result)
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
            foreach (var edge in ToolEdges)
            {
                edge.SetNameWithoutNotify(newName);
            }
        }
    }
}