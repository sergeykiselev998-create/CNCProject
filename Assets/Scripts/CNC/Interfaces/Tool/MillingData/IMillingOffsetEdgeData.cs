namespace CNC.Interfaces.Tool.MillingData
{
    public interface IMillingOffsetEdgeData
    {
        float Length { get; }
        float Diameter { get; }
        float TipAngle { get; }
        int NumberOfTeeth { get; }
        
        IMillingOffsetEdgeData Clone();
    }
}