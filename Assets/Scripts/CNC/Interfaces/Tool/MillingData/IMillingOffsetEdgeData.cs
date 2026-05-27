namespace CNC.Interfaces.Tool.MillingData
{
    public interface IMillingOffsetEdgeData
    {
        float Length { get; set; }
        float Diameter { get; set; }
        float TipAngle { get; set; }
        int NumberOfTeeth { get; set; }
    }
}