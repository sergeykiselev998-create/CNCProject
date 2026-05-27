namespace CNC.Interfaces.Tool.TurningData
{
    public interface ITurningOffsetEdgeData
    {
        float LengthX { get; set; }
        float LengthZ  { get; set; }
        float Radius { get; set; }
        float HolderAngle { get; set; }
        int InsertAngle { get; set; }
        float InsertLength { get; set; }
    }
}