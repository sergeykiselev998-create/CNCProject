using System;
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
        void Initialize(int location, TTool tool, SortedDictionary<int, TContent>  toolEdges);
        void UpdateVisual(SlotLocationType slotLocationType);
        void AddEdge(int edgeIndex, TContent edge);
        void RemoveEdge(int edgeIndex);
        void SetParent(Transform parent);
        void RemoveEdgesAndReset();
        void Destroy();
        int Id { get; }
    }
}