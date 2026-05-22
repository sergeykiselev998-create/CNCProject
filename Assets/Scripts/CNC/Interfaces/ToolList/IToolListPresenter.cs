using System;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Events;
using CNC.Interfaces.Views;

namespace CNC.Interfaces.ToolList
{
    /// <summary>
    /// Interface for the tool list presenter
    /// </summary>
    public interface IToolListPresenter<T> : IDisposable where T : ITool
    {
        IToolListModel<T> Model { get; }
        IToolListView View { get; }
        IEventBus EventBus { get; }
        
        void Initialize();
    }
}




