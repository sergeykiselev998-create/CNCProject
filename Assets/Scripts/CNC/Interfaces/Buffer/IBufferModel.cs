using System;
using System.Collections.Generic;
using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Buffer
{
    /// <summary>
    /// Interface for the buffer model
    /// </summary>
    public interface IBufferModel<T> where T : IMainData
    {
        bool TryAddTool(int toolId);
        bool TryRemoveTool(int toolId);
        bool TryGetTool(int id, out T tool);
        List<int> GetTools();
        T GetEmptyTool();
        bool Contains(int toolId);
    }

}

