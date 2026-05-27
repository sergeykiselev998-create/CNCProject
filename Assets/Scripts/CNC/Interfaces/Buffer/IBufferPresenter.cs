using System;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Events;

namespace CNC.Interfaces.Buffer
{
    public interface IBufferPresenter<TTool> : IDisposable where TTool : IMainData
    {
        IBufferModel<TTool> Model { get; }
        IBufferView<TTool> View { get; }
        IEventBus EventBus { get; }

        void Initialize();
    }

}


