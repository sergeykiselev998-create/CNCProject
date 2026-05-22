namespace CNC.Interfaces.Tool
{
    /// <summary>
    /// Interface for turning tools
    /// </summary>
    public interface ITurningTool : ITool
    {
        float ShiftX { get; }
        float ShiftY { get; }
    }
}

