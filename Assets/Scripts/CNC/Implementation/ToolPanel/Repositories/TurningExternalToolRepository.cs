using CNC.Implementation.Config;
using CNC.Implementation.Tool;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.ToolPanel.Repositories
{
    public class TurningExternalToolRepository : ExternalToolRepository<ITurningTool>
    {
        public TurningExternalToolRepository(ExternalToolConfig config) : base(config) { }
    
        protected override ITurningTool Get(int id)
        {
            string section = $"{_config.ToolSectionPrefix}{id}";
            string title = _reader.ReadString(section, _config.TitleKey, "");
        
            if (string.IsNullOrEmpty(title)) return null;
        
            bool enabled = _reader.ReadBoolean(section, _config.Enabled, false);
            if (!enabled) return null;
        
            float shiftX = _reader.ReadFloat(section, _config.Param1Key, 0f);
            float shiftY = _reader.ReadFloat(section, _config.Param2Key, 0f);
        
            return new TurningTool(id, title, 0);
        }
    }
}