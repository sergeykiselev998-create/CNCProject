using System.Collections.Generic;
using CNC.Enums;
using UnityEngine;
using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Views
{
    public interface ISlotControl<TContent, in TTool>
        where TContent : MonoBehaviour, ISlotView<TTool>
        where TTool : IMainData
    {
        void Initialize(int location, TTool tool, List<TContent> toolEdges);
        void UpdateVisual(SlotLocationType slotLocationType);
        void SetParent(Transform parent);
        int Id { get; }
    }
}