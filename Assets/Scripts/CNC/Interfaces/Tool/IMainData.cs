using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Tool
{
    public interface IMainData : IToolControl, IToolData
    {
        int CutterType { get; }
        string ToolName { get; set; }
        
        int CountEdges { get; }
        int[] GetEdges { get; }
    }
}