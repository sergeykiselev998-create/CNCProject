namespace CNC.Implementation.Config
{
    public class ExternalToolConfig : BaseConfig
    {
        public string ToolSectionPrefix { get; set; } = string.Empty;
        public string TitleKey { get; set; } = "Title";
        public string Enabled { get; set; } = "Enabled";
        public string Param1Key { get; set; } = string.Empty;
        public string Param2Key { get; set; } = string.Empty;
    }
}