namespace CNC.Interfaces.Config
{
    public interface IConfigReader
    {
        int ReadInt(string section, string key, int defaultValue);
        float ReadFloat(string section, string key, float defaultValue);
        string ReadString(string section, string key, string defaultValue);
        bool ReadBoolean(string section, string key, bool defaultValue);
        bool SectionExists(string section);
    }
}