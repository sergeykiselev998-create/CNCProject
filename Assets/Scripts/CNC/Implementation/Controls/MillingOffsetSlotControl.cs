using CNC.Implementation.Slots;
using CNC.Interfaces.Tool;
using CNC.Utils;
using UnityEngine.Events;

namespace CNC.Implementation.Controls
{
    public class MillingOffsetSlotControl : BaseSlotControl<MillingOffsetSlot, IMillingTool>
    {
        public event UnityAction<int, int, float> OnLengthChanged;
        public event UnityAction<int, int> OnLengthChangeFailed;
        
        public event UnityAction<int, int, float> OnDiameterChanged;
        public event UnityAction<int, int> OnDiameterChangeFailed;

        protected override void AddListeners()
        {
            foreach (var edge in ToolEdges)
            {
                edge.OnLengthChanged += HandleLengthChange;
                edge.OnDiameterChanged += HandleDiameterChange;
            }
        }

        protected override void RemoveListeners()
        {
            foreach (var edge in ToolEdges)
            {
                edge.OnLengthChanged -= HandleLengthChange;
                edge.OnDiameterChanged -= HandleDiameterChange;
            }
        }

        private void HandleLengthChange(int edgeIndex, string value)
        {
            if (TextFormatter.TryParseFloat(value, out var result))
            {
                OnLengthChanged?.Invoke(Tool.Id, edgeIndex, result);
            }
            else
            {
                OnLengthChangeFailed?.Invoke(Tool.Id, edgeIndex);
            }
        }
        
        private void HandleDiameterChange(int edgeIndex, string value)
        {
            if (TextFormatter.TryParseFloat(value, out var result))
            {
                OnDiameterChanged?.Invoke(Tool.Id, edgeIndex, result);
            }
            else
            {
                OnDiameterChangeFailed?.Invoke(Tool.Id, edgeIndex);
            }
        }

        public void SetLengthWithoutNotify(int edgeIndex, float length)
        {
            if (TryGetEdge(edgeIndex, out var edge))
                edge.SetLengthWithoutNotify(TextFormatter.Format(length));
        }

        public void SetDiameterWithoutNotify(int edgeIndex, float diameter)
        {
            if (TryGetEdge(edgeIndex, out var edge))
                edge.SetDiameterWithoutNotify(TextFormatter.Format(diameter));
        }

        private bool TryGetEdge(int edgeIndex, out MillingOffsetSlot edge)
        {
            edgeIndex--;
            if (edgeIndex < 0 || edgeIndex >= ToolEdges.Count)
            {
                edge = null;
                return false;
            }
    
            edge = ToolEdges[edgeIndex];
            return edge != null;
        }
    }
}