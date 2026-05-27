using System;
using CNC.Interfaces.Tool;

namespace CNC.Interfaces.ToolPanel
{
    public interface IToolPanelPresenter<T> : IDisposable where T : IMainData
    {
        void Initialize();
        void RemoveEdge(int location, int edgeIndexToRemove);
    }
}