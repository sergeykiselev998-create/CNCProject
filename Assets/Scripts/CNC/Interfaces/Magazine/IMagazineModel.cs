using System.Collections.Generic;
using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Magazine
{
    /// <summary>
    /// Interface for the magazine model
    /// </summary>
    public interface IMagazineModel<T> where T : IMainData
    {
        List<int> GetLocations();
        bool TryLoad(int location, int toolId);
        bool TryUnload(int location);
        bool IsSlotFree(int location);
        bool TryGetToolId(int location, out int toolId);
        bool TryGetTool(int location, out T tool);

        T GetEmptyTool();
    }
}


