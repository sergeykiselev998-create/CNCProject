using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;

namespace CNC.Interfaces.Buffer
{
    /// <summary>
    /// Interface for the buffer view
    /// </summary>
    public interface IBufferView<in TTool>
        where TTool : ITool
    {
        void AddBuffer(int location, TTool tool);
    }
}



