using System.Collections.Generic;
using CNC.Implementation.Controls;
using CNC.Implementation.Slots;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;
using UnityEngine;

namespace CNC.Interfaces.Factories
{
    public interface ISlotControlFactory<TControl, TContent, in TTool>
        where TControl : MonoBehaviour, ISlotControl<TContent,TTool>
        where TContent : MonoBehaviour, ISlotView<TTool>
        where TTool : ITool
    {
        ISlotControl<TContent, TTool> CreateSlotControl(TTool tool);
    }
}