using CNC.Interfaces.Tool.MillingData;

namespace CNC.Implementation.ToolData
{
    public class MillingWearEdgeData : IMillingWearEdgeData
    {
        public float WearLength { get; set; }
        public float WearDiameter { get; set; }
        
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