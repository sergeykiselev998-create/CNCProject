using System;
using CNC.Interfaces.Tool;
using UnityEngine.Events;

namespace CNC.Interfaces.ToolHolder
{
    public interface IToolHolderModel<TTool> 
        where TTool : IMainData
    {
        TTool CurrentTool { get; }
        int CurrentLocation { get; }
        int CurrentToolId { get; }
        bool HasTool { get; }
        bool TryLoadTool(int location, int toolId, out TTool tool);
        void UnloadTool();
        bool TryGetTool(out TTool tool);
        TTool GetEmptyTool();
    }
}