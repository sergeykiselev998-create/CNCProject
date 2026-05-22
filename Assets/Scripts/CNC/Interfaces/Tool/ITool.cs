using System.Collections.Generic;
using CNC.Interfaces.Offsets;

namespace CNC.Interfaces.Tool
{
    /// <summary>
    /// Base interface for all tools
    /// </summary>
    public interface ITool : IToolControl
    {
      //  int Id { get; }
        string ToolName { get; }
        Dictionary<int, IOffset> Offsets { get; }
    }
}


