using System;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Events;
using CNC.Interfaces.ToolList;
using CNC.Interfaces.Views;

namespace CNC.Interfaces.Magazine
{
    /// <summary>
    /// Interface for the magazine presenter
    /// </summary>
    public interface IMagazinePresenter<TTool> : IDisposable where TTool : ITool
    {
        IMagazineModel<TTool> Model { get; }
        IMagazineView<TTool> View { get; }
        IEventBus EventBus { get; }

        void Initialize();
    }
}




