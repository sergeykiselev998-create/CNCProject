using System.Collections.Generic;
using CNC.Enums;
using UnityEngine;
using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Views
{
    public interface ISlotControl<TContent, in TTool>
        where TContent : MonoBehaviour, ISlotView<TTool>
        where TTool : ITool
    {
        void Initialize(int location, TTool tool, List<TContent> toolEdges);
        void SetParent(Transform parent);
    }
}