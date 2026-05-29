using System.Collections.Generic;
using CNC.Enums;

namespace CNC.Interfaces.Tool.TurningData
{
    public interface ITurningWearData : IToolData
    {
        bool ToolDisabled { get; set; }
        TCWParameter TcwParameter { get; set; }
        SortedDictionary<int, ITurningWearEdgeData> WearEdgeData { get; }
    }
}