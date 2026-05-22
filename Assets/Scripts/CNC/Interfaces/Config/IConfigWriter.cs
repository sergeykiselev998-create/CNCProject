namespace CNC.Interfaces.Config
{
    public interface IConfigWriter
    {
        void WriteInt(string section, string key, int value);
        void WriteFloat(string section, string key, float value);
        void WriteString(string section, string key, string value);
        void WriteBoolean(string section, string key, bool value);
    }
}