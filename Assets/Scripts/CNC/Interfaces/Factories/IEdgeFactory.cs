using System.Collections.Generic;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;
using UnityEngine;

namespace CNC.Interfaces.Factories
{
    public interface IEdgeFactory<TContent, TTool>
    where TContent : MonoBehaviour, ISlotView<TTool>
    where TTool : IMainData
    {
        public List<TContent> CreateEdges(TTool tool);
    }
}