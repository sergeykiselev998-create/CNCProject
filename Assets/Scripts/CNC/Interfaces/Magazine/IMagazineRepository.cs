using System.Collections.Generic;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolPanel;

namespace CNC.Interfaces.Magazine
{
    /// <summary>
    /// Repository interface for magazine slots
    /// </summary>
    public interface IMagazineRepository<T> where T : IMainData
    {
        IToolRepository<T> ToolRepository { get; }
        Dictionary<int,int> Slots { get; }
        List<int> GetSlotsLocations();
        bool TryGetTool(int location, out T tool);
        bool TryGetToolId(int location, out int toolId);
        void Load(int location, int toolId);
        void Unload(int location);

        T GetEmptyTool();
    }
}
