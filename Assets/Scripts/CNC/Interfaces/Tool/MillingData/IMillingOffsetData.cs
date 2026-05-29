using System.Collections.Generic;
using CNC.Enums;

namespace CNC.Interfaces.Tool.MillingData
{
    public interface IMillingOffsetData : IToolData
    {
        bool Coolant1 { get; set; }
        bool Coolant2 { get; set; }
        SpindleDirection SpindleDirection { get; set; }
        SortedDictionary<int, IMillingOffsetEdgeData> OffsetEdgeData { get; }
    }
}