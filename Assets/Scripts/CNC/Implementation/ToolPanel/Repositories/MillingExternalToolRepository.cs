using CNC.Implementation.Config;
using CNC.Implementation.Tool;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.ToolPanel.Repositories
{
    public class MillingExternalToolRepository : ExternalToolRepository<IMillingTool>
    {
        public MillingExternalToolRepository(ExternalToolConfig config) : base(config) { }
    
        protected override IMillingTool Get(int id)
        {
            string section = $"{_config.ToolSectionPrefix}{id}";
            string title = _reader.ReadString(section, _config.TitleKey, "");
        
            if (string.IsNullOrEmpty(title)) return null;
        
            bool enabled = _reader.ReadBoolean(section, _config.Enabled, false);
            if (!enabled) return null;
        
            float diameter = _reader.ReadFloat(section, _config.Param1Key, 6.0f);
            int cutterType = (int)_reader.ReadFloat(section, _config.Param2Key, 0);
        
            return new MillingTool(id, title, diameter, cutterType);
        }
    }
}