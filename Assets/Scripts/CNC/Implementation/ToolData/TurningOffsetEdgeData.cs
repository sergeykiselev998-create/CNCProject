using CNC.Interfaces.Tool.TurningData;

namespace CNC.Implementation.ToolData
{
    public class TurningOffsetEdgeData : ITurningOffsetEdgeData
    {
        public float LengthX { get; set; }
        public float LengthZ { get; set; }
        public float Radius { get; set; }
        public float HolderAngle { get; set; }
        public int InsertAngle { get; set; }
        public float InsertLength { get; set; }
    }
}