namespace CNC.Interfaces.Offsets
{
    /// <summary>
    /// Interface for tool offsets
    /// </summary>
    public interface IOffset
    {
        float X { get; }
        float Y { get; }
        float Z { get; }
        float T { get; }
        
        void Set(float x, float y, float z, float t);
    }
}


