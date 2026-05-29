using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Tool
{
    public interface IMainData : IToolControl, IToolData
    {
        int CutterType { get; }
        string ToolName { get; }
        
        int CountEdges { get; }
        int[] GetEdges { get; }
    }
}