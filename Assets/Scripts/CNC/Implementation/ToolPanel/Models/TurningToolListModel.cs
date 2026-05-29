using CNC.Implementation.Tool;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolPanel;
using CNC.Utils;
using UnityEngine.Events;

namespace CNC.Implementation.ToolPanel.Models
{
    public class TurningToolListModel : ToolListModel<ITurningTool>
    {
        public TurningToolListModel(IToolRepository<ITurningTool> repository) : base(repository)
        {
        }
        
        public override void UpdateToolName(int id, string newName)
        {
            if (!TryGetTool(id, out var toolInterface))
                return;

            if(!toolInterface.TryCast<TurningTool,ITurningTool>(out var tool))
                return;
            
            tool.ToolName = newName;
            OnToolNameChangedHandler(id, newName);
        }
    }
}