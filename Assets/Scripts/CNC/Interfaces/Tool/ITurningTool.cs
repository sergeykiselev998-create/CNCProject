using CNC.Interfaces.Tool.TurningData;

namespace CNC.Interfaces.Tool
{
    /// <summary>
    /// Interface for turning tools
    /// </summary>
    public interface ITurningTool : IMainData, ITurningOffsetData, ITurningWearData, ITurningMagazineData
    {
    }
}

