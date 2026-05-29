using System.Collections.Generic;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;
using UnityEngine;

namespace CNC.Interfaces.Factories
{
    public interface ISlotFactory<TControl, TContent, in TTool> 
        where TControl : MonoBehaviour, ISlotControl<TContent,TTool>
        where TContent : MonoBehaviour, ISlotView<TTool>
        where TTool : IMainData
    {
        ISlotControl<TContent, TTool> CreateSlotControl();
        SortedDictionary<int, TContent>  CreateEdges(TTool tool);
        TContent CreateSingleEdge();
    }
}