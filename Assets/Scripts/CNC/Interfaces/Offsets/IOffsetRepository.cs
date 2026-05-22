using System.Collections.Generic;
using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Offsets
{
    /// <summary>
    /// Repository interface for tool offsets
    /// </summary>
    public interface IOffsetRepository<T> where T : ITool
    {
        Dictionary<(int, int), IOffset> Offsets { get; } // ((Id, D (Edges)), Offset)

        void LoadAll();
        void Load(IEnumerable<int> toolIds);
        void Save();
        bool TryGetOffset(int toolId, int offsetNumber, out IOffset offset);
        Dictionary<int, IOffset> GetOffsetsOrDefault(int toolId);
        void SetOffset(int toolId, int offsetNumber, IOffset offset);
        void Clear();
    }
}


