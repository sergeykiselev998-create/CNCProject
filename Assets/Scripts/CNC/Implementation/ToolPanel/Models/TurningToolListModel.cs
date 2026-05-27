using CNC.Implementation.Tool;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolPanel;
using UnityEngine;
using UnityEngine.Events;

namespace CNC.Implementation.ToolPanel.Models
{
    public class TurningToolListModel : ToolListModel<ITurningTool>
    {
        public TurningToolListModel(IToolRepository<ITurningTool> repository) : base(repository)
        {
        }
    }
}