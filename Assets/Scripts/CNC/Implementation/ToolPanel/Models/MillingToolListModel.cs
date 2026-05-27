using CNC.Implementation.Tool;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Tool.MillingData;
using CNC.Interfaces.ToolPanel;
using UnityEngine;
using UnityEngine.Events;

namespace CNC.Implementation.ToolPanel.Models
{
    public class MillingToolListModel : ToolListModel<IMillingTool>
    {
        public event UnityAction<int, int, float> OnLengthChanged; 
        public event UnityAction<int, int, float> OnDiameterChanged; 
        
        public MillingToolListModel(IToolRepository<IMillingTool> repository) : base(repository)
        {
        }
        
        public void UpdateLength(int id, int edge, float length)
        {
            if (!TryGetTool(id, out var tool))
                return;

            if (!TryGetOffset(tool, edge, out var offset))
                return;

            offset.Length = length;
            OnLengthChanged?.Invoke(id, edge, length);
        }
        
        public void UpdateDiameter(int id, int edge, float diameter)
        {
            if (!TryGetTool(id, out var tool))
                return;

            if (!TryGetOffset(tool, edge, out var offset))
                return;
            
            offset.Diameter = diameter;
            OnDiameterChanged?.Invoke(id, edge, diameter);
        }
        
        public bool TryGetOffset(IMillingTool tool, int edge, out IMillingOffsetEdgeData offset)
        {
            var result = tool.OffsetEdgeData.TryGetValue(edge, out offset);
            
            if (!result)
                Debug.Log($"[{GetType().Name}] Edge {edge} doesn't exist in tool {tool.Id}");
            
            return result;
        }
    }
}