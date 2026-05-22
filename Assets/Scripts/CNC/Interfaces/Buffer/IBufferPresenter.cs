using System;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Events;
using CNC.Interfaces.ToolList;
using CNC.Interfaces.Views;

namespace CNC.Interfaces.Buffer
{
    public interface IBufferPresenter<TTool> : IDisposable where TTool : ITool
    {
        IBufferModel<TTool> Model { get; }
        IBufferView<TTool> View { get; }
        IEventBus EventBus { get; }

        void Initialize();
    }

}


