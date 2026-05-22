using System.Collections.Generic;
using CNC.Implementation.Config;
using CNC.Implementation.Offsets;
using CNC.Implementation.Tool;
using CNC.Interfaces.Offsets;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.ToolList
{
    public class MillingToolRepository : ToolRepository<IMillingTool>
    {
        public MillingToolRepository(ToolConfig config, IOffsetRepository<IMillingTool> offsetRepository) 
            : base(config, offsetRepository)
        {
        }
        
        public override IMillingTool CreateEmptyTool()
        {
            var emptyOffsets = new Dictionary<int, IOffset>();
            emptyOffsets[1] = new Offset();
        
            return new MillingTool(
                id: -1,
                toolName: string.Empty,
                offsets: emptyOffsets,
                diameter: 0,
                cutterType: 0
            );
        }
        
        protected override IMillingTool CreateTool(int id)
        {
            string section = $"{_config.ToolSectionPrefix}{id}";
            string title = _reader.ReadString(section, _config.TitleKey, "");
            
            if (string.IsNullOrEmpty(title)) 
                return null;

            bool enabled = _reader.ReadBoolean(section, _config.Enabled, false);
            
            if (enabled == false) 
                return null;
            
            float param1 = _reader.ReadFloat(section, _config.Param1Key, 6.0f);
            float param2 = _reader.ReadFloat(section, _config.Param2Key, 0);
            
            var toolOffsets = Offsets.GetOffsetsOrDefault(id);
            
            return new MillingTool(id, title, toolOffsets, param1, (int)param2);
        }
    }
}