using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;

namespace CNC.Interfaces.Buffer
{
    /// <summary>
    /// Interface for the buffer view
    /// </summary>
    public interface IBufferView<in TTool>
        where TTool : IMainData
    {
        void AddBuffer(int location, TTool tool);
        void UnloadBuffer(int toolId);
        void LoadBuffer(int location, TTool tool);
    }
}



