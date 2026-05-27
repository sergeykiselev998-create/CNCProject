using CNC.Interfaces.Tool.TurningData;

namespace CNC.Implementation.ToolData
{
    public class TurningWearEdgeData : ITurningWearEdgeData
    {
        public float WearLengthX { get; set; }
        public float WearLengthZ { get; set; }
        public float WearRadius { get; set; }
        
        public float ToolLife { get; set; }
        public float ToolNominalLife { get; set; }
        public float ToolLimitLife { get; set; }
        
        public float ToolQuantity { get; set; }
        public float ToolNominalQuantity { get; set; }
        public float ToolLimitQuantity { get; set; }
        
        public float ToolWear { get; set; }
        public float ToolNominalWear { get; set; }
        public float ToolLimitWear { get; set; }
    }
}