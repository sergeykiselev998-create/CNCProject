using CNC.Interfaces.Events;
using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Machine
{
    /// <summary>
    /// Interface for the machine presenter
    /// </summary>
    public interface IMachinePresenter<T> where T : ITool
    {
        IMachine<T> Machine { get; }
        IEventBus EventBus { get; }
        
        void Initialize();
    }
}




