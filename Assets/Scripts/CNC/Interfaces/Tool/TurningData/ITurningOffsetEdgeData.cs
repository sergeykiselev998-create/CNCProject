namespace CNC.Interfaces.Tool.TurningData
{
    public interface ITurningOffsetEdgeData
    {
        float LengthX { get; }
        float LengthZ  { get; }
        float Radius { get; }
        float HolderAngle { get; set; }
        int InsertAngle { get; set; }
        float InsertLength { get; set; }
    }
}