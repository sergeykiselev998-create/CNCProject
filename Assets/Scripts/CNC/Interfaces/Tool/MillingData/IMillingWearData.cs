using System.Collections.Generic;
using CNC.Enums;

namespace CNC.Interfaces.Tool.MillingData
{
    public interface IMillingWearData : IToolData
    {
        bool ToolDisabled { get; set; }
        TCWParameter TcwParameter { get; set; }
        Dictionary<int, IMillingWearEdgeData> WearEdgeData { get; }
    }
}