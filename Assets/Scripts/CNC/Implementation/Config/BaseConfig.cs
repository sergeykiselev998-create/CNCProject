using CNC.Interfaces.Config;
using UnityEngine;
using static System.IO.File;

namespace CNC.Implementation.Config
{
    public abstract class BaseConfig : IIniConfig
    {
        public string FilePath { get; set; } = string.Empty;
        public string MainSection { get; set; } = "Main";
        public string CountKey { get; set; } = string.Empty;

        protected BaseConfig() { }

        protected BaseConfig(string filePath, string mainSection, string countKey)
        {
            FilePath = filePath;
            MainSection = mainSection;
            CountKey = countKey;
        }

        public bool FileExists
        {
            get
            {
                if (Exists(FilePath))
                    return true;
                
                Debug.LogWarning($"File doesn't exist! Path: {FilePath}");
                return false;
            }
        }
    }
}