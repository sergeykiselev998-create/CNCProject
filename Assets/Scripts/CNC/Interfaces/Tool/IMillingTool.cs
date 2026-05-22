namespace CNC.Interfaces.Tool
{
    /// <summary>
    /// Interface for milling tools
    /// </summary>
    public interface IMillingTool : ITool
    {
        int CutterType { get; }
        float Diameter { get; }
    }
}


