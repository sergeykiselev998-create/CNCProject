using System.Collections.Generic;
using CNC.Enums;

namespace CNC.Interfaces.Tool.TurningData
{
    public interface ITurningOffsetData : IToolData
    {
        bool Coolant1 { get; set; }
        bool Coolant2 { get; set;  }
        SpindleDirection SpindleDirection { get; set;  }
        HolderDirection HolderDirection { get;  set; }
        Dictionary<int, ITurningOffsetEdgeData> OffsetEdgeData { get; }
    }
}