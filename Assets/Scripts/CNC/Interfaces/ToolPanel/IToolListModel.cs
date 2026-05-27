using CNC.Interfaces.Tool;
using UnityEngine.Events;

namespace CNC.Interfaces.ToolPanel
{
    /// <summary>
    /// Interface for the tool list model
    /// </summary>
    public interface IToolListModel<TTool> where TTool : IMainData
    {
        event UnityAction<int, string> OnToolNameChanged;
        void UpdateToolName(int id, string newName);
        bool TryGetTool(int id, out TTool tool);
    }
}




