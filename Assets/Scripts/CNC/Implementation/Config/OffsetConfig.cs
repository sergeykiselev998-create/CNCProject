using CNC.Interfaces.Config;

namespace CNC.Implementation.Config
{
    public class OffsetConfig : BaseConfig
    {
        public string ToolSectionPrefix { get; set; } = "T";
        public string EdgesCountKey { get; set; } = "EdgesCount";
        public string OffsetKeyPrefix { get; set; } = "D";

        public OffsetConfig() { }

        public OffsetConfig(string filePath, string mainSection, string countKey, 
            string toolSectionPrefix, string edgesCountKey, string offsetKeyPrefix) 
            : base(filePath, mainSection, countKey)
        {
            ToolSectionPrefix = toolSectionPrefix;
            EdgesCountKey = edgesCountKey;
            OffsetKeyPrefix = offsetKeyPrefix;
        }
    }
}