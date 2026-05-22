using System.Runtime.InteropServices;
using CNC.Interfaces.Config;

namespace CNC.Implementation.Config
{
    public class IniWriter : IConfigWriter
    {
        private readonly string _filePath;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileString(string section, string key, string value, string filePath);

        public IniWriter(string filePath)
        {
            _filePath = filePath;
        }

        public void WriteInt(string section, string key, int value)
        {
            WritePrivateProfileString(section, key, value.ToString(), _filePath);
        }

        public void WriteFloat(string section, string key, float value)
        {
            var strValue = value.ToString(System.Globalization.CultureInfo.InvariantCulture);
            WritePrivateProfileString(section, key, strValue, _filePath);
        }

        public void WriteString(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _filePath);
        }
        
        public void WriteBoolean(string section, string key, bool value)
        {
            WritePrivateProfileString(section, key, value ? "True" : "False", _filePath);
        }
    }
}