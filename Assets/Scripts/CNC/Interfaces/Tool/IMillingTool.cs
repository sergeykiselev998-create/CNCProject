using CNC.Interfaces.Tool.MillingData;

namespace CNC.Interfaces.Tool
{
    /// <summary>
    /// Interface for milling tools
    /// </summary>
    public interface IMillingTool : IMainData, IMillingOffsetData, IMillingWearData, IMillingMagazineData
    {
    }
}


