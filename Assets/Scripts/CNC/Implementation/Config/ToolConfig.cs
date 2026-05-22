using CNC.Interfaces.Config;

namespace CNC.Implementation.Config
{
    public class ToolConfig : BaseConfig
    {
        public string ToolSectionPrefix { get; set; } = string.Empty;
        public string TitleKey { get; set; } = "Title";
        public string Enabled { get; set; } = "Enabled";
        public string Param1Key { get; set; } = string.Empty;
        public string Param2Key { get; set; } = string.Empty;  

        public ToolConfig() { }

        public ToolConfig(string filePath, string mainSection, string countKey, 
            string toolSectionPrefix, string titleKey, string enabled, string param1Key, string param2Key) 
            : base(filePath, mainSection, countKey)
        {
            ToolSectionPrefix = toolSectionPrefix;
            TitleKey = titleKey;
            Enabled = enabled;
            Param1Key = param1Key;
            Param2Key = param2Key;
        }
    }
}