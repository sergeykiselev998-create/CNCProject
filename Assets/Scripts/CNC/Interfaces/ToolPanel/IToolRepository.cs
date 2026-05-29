using System.Collections.Generic;
using CNC.Interfaces.Tool;

namespace CNC.Interfaces.ToolPanel
{
    /// <summary>
    /// Repository interface for tools
    /// </summary>
    public interface IToolRepository<T> where T : IMainData
    {
        Dictionary<int, T> Tools { get; }
        T EmptyTool { get; }
        void Load();
        bool TryGetTool(int id, out T tool);
        void AddTool(T tool);
        void RemoveTool(int id);
        void SaveAdditional();
    }
}


