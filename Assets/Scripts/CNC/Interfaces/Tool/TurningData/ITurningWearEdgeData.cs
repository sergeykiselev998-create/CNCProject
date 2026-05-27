namespace CNC.Interfaces.Tool.TurningData
{
    public interface ITurningWearEdgeData
    {
        float WearLengthX { get; set; }
        float WearLengthZ { get; set; }
        float WearRadius { get; set; }
        
        float ToolLife { get; set; }
        float ToolNominalLife { get; set; }
        float ToolLimitLife { get; set; }
        
        float ToolQuantity { get; set; }
        float ToolNominalQuantity { get; set; }
        float ToolLimitQuantity { get; set; }
        
        float ToolWear { get; set; }
        float ToolNominalWear { get; set; }
        float ToolLimitWear { get; set; }
    }
}