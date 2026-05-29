namespace CNC.Interfaces.Tool.MillingData
{
    public interface IMillingWearEdgeData
    {
        float WearLength { get; set; }
        float WearDiameter { get; set; }
        
        float ToolLife { get; set; }
        float ToolNominalLife { get; set; }
        float ToolLimitLife { get; set; }
        
        float ToolQuantity { get; set; }
        float ToolNominalQuantity { get; set; }
        float ToolLimitQuantity { get; set; }
        
        float ToolWear { get; set; }
        float ToolNominalWear { get; set; }
        float ToolLimitWear { get; set; }
        
        IMillingWearEdgeData Clone();
    }
}