using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;

namespace CNC.Interfaces.Machine
{
    /// <summary>
    /// Interface for the machine view
    /// </summary>
    public interface IMachineView : ISlotView<ITool>
    {
    }
}