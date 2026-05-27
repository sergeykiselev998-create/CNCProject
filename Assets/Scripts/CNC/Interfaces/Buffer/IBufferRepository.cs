using System.Collections.Generic;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolPanel;

namespace CNC.Interfaces.Buffer
{
    /// <summary>
    /// Repository interface for buffer slots
    /// </summary>
    public interface IBufferRepository<T> where T : IMainData
    {
        IToolRepository<T> ToolRepository { get; }
        List<int> Slots { get; } // ToolId, -1 = empty
    
        void Add(int toolId);
        void Remove(int toolId);
        bool TryGetTool(int toolId, out T tool);
        void SetTools(IEnumerable<int> bufferTools);
        List<int> GetTools();
        T GetEmptyTool();
        bool Contains(int toolId);
    }
}