using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using CNC.Interfaces.Config;
using UnityEngine;

namespace CNC.Implementation.Config
{
    public class IniReader : IConfigReader
    {
        private readonly string _filePath;
    
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileInt(string section, string key, int defaultValue, string filePath);
    
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder result, int size, string filePath);
    
        public IniReader(string filePath)
        {
            _filePath = filePath;
        }
    
        public int ReadInt(string section, string key, int defaultValue)
        {
            return GetPrivateProfileInt(section, key, defaultValue, _filePath);
        }
    
        public float ReadFloat(string section, string key, float defaultValue)
        {
            var result = new StringBuilder(255);
            var defaultStr = defaultValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
            
            GetPrivateProfileString(section, key, defaultStr, result, 255, _filePath);
            
            if (float.TryParse(result.ToString(), 
                System.Globalization.NumberStyles.Float, 
                System.Globalization.CultureInfo.InvariantCulture, 
                out float value))
            {
                return value;
            }
            
            return defaultValue;
        }
    
        public string ReadString(string section, string key, string defaultValue)
        {
            var result = new StringBuilder(255);
            GetPrivateProfileString(section, key, defaultValue, result, 255, _filePath);
            return result.ToString();
        }
        
        public bool ReadBoolean(string section, string key, bool defaultValue)
        {
            var result = new StringBuilder(255);
            // Преобразуем default value в строку ("True" или "False")
            var defaultStr = defaultValue ? "True" : "False";
        
            GetPrivateProfileString(section, key, defaultStr, result, 255, _filePath);
        
            if (bool.TryParse(result.ToString(), out bool value))
            {
                return value;
            }
        
            return defaultValue;
        }
    
        public bool SectionExists(string section)
        {
            var result = new StringBuilder(255);
            GetPrivateProfileString(section, null, null, result, 255, _filePath);
            return result.Length > 0;
        }
    }
}
