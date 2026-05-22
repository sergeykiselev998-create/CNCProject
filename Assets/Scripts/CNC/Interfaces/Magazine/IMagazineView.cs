using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;

namespace CNC.Interfaces.Magazine
{
    /// <summary>
    /// Interface for the magazine view
    /// </summary>
    public interface IMagazineView<in TTool>
    where TTool : ITool
    {
        void AddMagazine(int location, TTool tool);
    }
}




