using System.Collections.Generic;
using CNC.Implementation.Config;
using CNC.Implementation.Offsets;
using CNC.Implementation.Tool;
using CNC.Interfaces.Offsets;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.ToolList
{
    public class TurningToolRepository : ToolRepository<ITurningTool>
    {
        public TurningToolRepository(ToolConfig config, IOffsetRepository<ITurningTool> offsetRepository) 
            : base(config, offsetRepository)
        {
        }
        
        public override ITurningTool CreateEmptyTool()
        {
            var emptyOffsets = new Dictionary<int, IOffset>();
            emptyOffsets[1] = new Offset();
        
            return new TurningTool(
                id: -1,
                toolName: string.Empty,
                offsets: emptyOffsets,
                shiftX: 0,
                shiftY: 0
            );
        }

        protected override ITurningTool CreateTool(int id)
        {
            string section = $"{_config.ToolSectionPrefix}{id}";
            string title = _reader.ReadString(section, _config.TitleKey, "");
            
            if (string.IsNullOrEmpty(title)) 
                return null;
            
            bool enabled = _reader.ReadBoolean(section, _config.Enabled, false);
            
            if (enabled == false) 
                return null;
            
            float param1 = _reader.ReadFloat(section, _config.Param1Key, 0f);
            float param2 = _reader.ReadFloat(section, _config.Param2Key, 0f);
            
            var toolOffsets = Offsets.GetOffsetsOrDefault(id);
            
            return new TurningTool(id, title, toolOffsets, param1, param2);
        }
    }
}