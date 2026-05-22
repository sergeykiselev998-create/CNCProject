using System.Collections.Generic;
using CNC.Interfaces.Offsets;
using CNC.Interfaces.Tool;

namespace CNC.Interfaces.ToolList
{
    /// <summary>
    /// Repository interface for tools
    /// </summary>
    public interface IToolRepository<T> where T : ITool
    {
        IOffsetRepository<T> Offsets { get; }
        Dictionary<int, T> Tools { get; }
        T CreateEmptyTool();
        void Load();
        bool TryGetTool(int id, out T tool);
        void Add(T tool);
        void Remove(int id);
    }
}


