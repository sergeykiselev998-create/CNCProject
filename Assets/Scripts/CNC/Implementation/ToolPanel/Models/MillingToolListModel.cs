using CNC.Implementation.Tool;
using CNC.Implementation.ToolData;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Tool.MillingData;
using CNC.Interfaces.ToolPanel;
using UnityEngine.Events;
using CNC.Utils;

namespace CNC.Implementation.ToolPanel.Models
{
    public class MillingToolListModel : ToolListModel<IMillingTool>
    {

        public event UnityAction<int, int, float> OnLengthChanged; 
        public event UnityAction<int, int, float> OnDiameterChanged;

        public event UnityAction<int, int> OnEdgeAdded;
        
        public MillingToolListModel(IToolRepository<IMillingTool> repository) : base(repository)
        {
        }
        
        public override void UpdateToolName(int id, string newName)
        {
            if (!TryGetTool(id, out var toolInterface))
                return;

            if(!toolInterface.TryCast(out MillingTool tool))
                return;
            
            tool.ToolName = newName;
            OnToolNameChangedHandler(id, newName);
        }
        
        public void UpdateLength(int id, int edge, float length)
        {
            if (!TryGetTool(id, out var tool))
                return;

            if (!TryGetOffset(tool, edge, out var offsetInterface))
                return;
            
            if(!offsetInterface.TryCast(out MillingOffsetEdgeData offset))
                return;

            offset.Length = length;
            OnLengthChanged?.Invoke(id, edge, length);
        }
        
        public void UpdateDiameter(int id, int edge, float diameter)
        {
            if (!TryGetTool(id, out var tool))
                return;

            if (!TryGetOffset(tool, edge, out var offsetInterface))
                return;
            
            if(!offsetInterface.TryCast(out MillingOffsetEdgeData offset))
                return;
            
            offset.Diameter = diameter;
            OnDiameterChanged?.Invoke(id, edge, diameter);
        }
        
        public bool TryGetOffset(IMillingTool tool, int edge, out IMillingOffsetEdgeData offset)
        {
            return tool.OffsetEdgeData.TryGetValue(edge, out offset);
        }

        public void AddEdge(int id)
        {
            if (!TryGetTool(id, out var toolInterface))
                return;

            if(!toolInterface.TryCast(out MillingTool tool))
                return;
            
            
        }
    }
}