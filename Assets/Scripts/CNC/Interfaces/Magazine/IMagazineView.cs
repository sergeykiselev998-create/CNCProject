using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Magazine
{
    /// <summary>
    /// Interface for the magazine view
    /// </summary>
    public interface IMagazineView<in TTool>
    where TTool : IMainData
    {
        void AddMagazine(int location, TTool tool);
        void UnloadMagazine(int location, TTool emptyTool);
        void LoadMagazine(int location, TTool tool);
    }
}




