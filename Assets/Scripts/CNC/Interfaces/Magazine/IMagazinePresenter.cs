using System;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Events;

namespace CNC.Interfaces.Magazine
{
    /// <summary>
    /// Interface for the magazine presenter
    /// </summary>
    public interface IMagazinePresenter<TTool> : IDisposable where TTool : IMainData
    {
        IMagazineModel<TTool> Model { get; }
        IMagazineView<TTool> View { get; }
        IEventBus EventBus { get; }

        void Initialize();
    }
}




