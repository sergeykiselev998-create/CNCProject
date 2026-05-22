using System;
using CNC.Interfaces.Events;
using CNC.Interfaces.Strategies;
using CNC.Interfaces.Tool;

namespace CNC.Interfaces.ToolHolder
{
    public interface IToolHolderPresenter<TTool>: IDisposable where TTool : ITool
    {
        IToolHolderModel<TTool> Model { get; }
        IToolHolderView<TTool> View { get; }
        IToolHolderStrategy Strategy { get; }
        IEventBus EventBus { get; }
        void Initialize();
    }
}