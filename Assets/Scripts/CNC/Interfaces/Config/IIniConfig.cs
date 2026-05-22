namespace CNC.Interfaces.Config
{
    public interface IIniConfig
    {
        string FilePath { get; }
        string MainSection { get; }
        string CountKey { get; }
        
        bool FileExists { get; }
    }
}