using System.Collections.Generic;
using CNC.Implementation.Factories;
using CNC.Implementation.Slots;
using CNC.Interfaces.Buffer;
using CNC.Interfaces.Magazine;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolHolder;
using CNC.Interfaces.Views;
using UnityEngine;

namespace CNC.Interfaces.ToolPanel
{
    /// <summary>
    /// Interface for tool panel view
    /// </summary>
    public interface IToolPanelView<TControl, TContent,in TTool> : 
        IBufferView<TTool>, 
        IMagazineView<TTool>,
        IToolHolderView<TTool>
        where TControl : MonoBehaviour, ISlotControl<TContent,TTool>
        where TContent : MonoBehaviour, ISlotView<TTool>
        where TTool : IMainData
    {

    }

}

