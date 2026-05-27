using CNC.Enums;
using CNC.Implementation.Config;
using CNC.Interfaces.Config;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.ToolPanel.Repositories
{
    public class TurningAdditionalToolRepository : AdditionalToolRepository<ITurningTool>
    {
        public TurningAdditionalToolRepository(AdditionalToolConfig config) : base(config) { }
        
        protected override void SaveDataToWriter(int id, ITurningTool tool, IConfigWriter writer)
        {
            var sectionKey = $"{_config.ToolSectionPrefix}{id}";
            
            writer.WriteInt(sectionKey, _config.EdgesCountKey, tool.CountEdges);
            
            string offsetBoolData = $"{ (tool.Coolant1 ? 1 : 0) },{ (tool.Coolant2 ? 1 : 0) }";
            writer.WriteString(sectionKey, _config.OffsetDataBoolKey, offsetBoolData);
            
            string magazineData = $"{ (tool.MagazineLocationDisabled ? 1 : 0) },{ (tool.ToolOversize ? 1 : 0) }";
            writer.WriteString(sectionKey, _config.MagazineDataBoolKey, magazineData);
            
            string offsetIntData = $"{ (int)tool.SpindleDirection },{ (int)tool.HolderDirection }";
            writer.WriteString(sectionKey, _config.OffsetDataIntKey, offsetIntData);
            
            string wearData = $"{ (tool.ToolDisabled ? 1 : 0) },{ (int)tool.TcwParameter }";
            writer.WriteString(sectionKey, _config.WearDataIntKey, wearData);
            
            for (int edge = 1; edge <= tool.CountEdges; edge++)
            {
                if (tool.OffsetEdgeData.TryGetValue(edge, out var offsetEdge))
                {
                    string offsetEdgeFloat = $"{ToInvariant(offsetEdge.LengthX)}," +
                                             $"{ToInvariant(offsetEdge.LengthZ)}," +
                                             $"{ToInvariant(offsetEdge.Radius)}," +
                                             $"{ToInvariant(offsetEdge.HolderAngle)}," +
                                             $"{offsetEdge.InsertAngle}," +
                                             $"{ToInvariant(offsetEdge.InsertLength)}";
                    writer.WriteString(sectionKey, _config.GetOffsetEdgeFloatKey(edge), offsetEdgeFloat);
                }
                
                if (tool.WearEdgeData.TryGetValue(edge, out var wearEdge))
                {
                    string wearEdgeData = $"{ToInvariant(wearEdge.WearLengthX)}," +
                                          $"{ToInvariant(wearEdge.WearLengthZ)}," +
                                          $"{ToInvariant(wearEdge.WearRadius)}," +
                                          $"{ToInvariant(wearEdge.ToolLife)}," +
                                          $"{ToInvariant(wearEdge.ToolNominalLife)}," +
                                          $"{ToInvariant(wearEdge.ToolLimitLife)}," +
                                          $"{ToInvariant(wearEdge.ToolQuantity)}," +
                                          $"{ToInvariant(wearEdge.ToolNominalQuantity)}," +
                                          $"{ToInvariant(wearEdge.ToolLimitQuantity)}," +
                                          $"{ToInvariant(wearEdge.ToolWear)}," +
                                          $"{ToInvariant(wearEdge.ToolNominalWear)}," +
                                          $"{ToInvariant(wearEdge.ToolLimitWear)}";
                    writer.WriteString(sectionKey, _config.GetWearEdgeFloatKey(edge), wearEdgeData);
                }
            }
        }

        
        protected override void LoadBoolData(string sectionKey, ITurningTool tool)
        {
            string offsetBoolStr = _reader.ReadString(sectionKey, _config.OffsetDataBoolKey, "");
            if (!string.IsNullOrEmpty(offsetBoolStr))
            {
                var values = offsetBoolStr.Split(',');
                if (values.Length >= _config.OffsetDataValueCount)
                {
                    int.TryParse(values[0], out int coolant1);
                    int.TryParse(values[1], out int coolant2);
                    
                    tool.Coolant1 = coolant1 == 1;
                    tool.Coolant2 = coolant2 == 1;
                }
            }
            
            string magazineDataStr = _reader.ReadString(sectionKey, _config.MagazineDataBoolKey, "");
            if (!string.IsNullOrEmpty(magazineDataStr))
            {
                var values = magazineDataStr.Split(',');
                if (values.Length >= _config.MagazineDataBoolValueCount)
                {
                    int.TryParse(values[0], out int magazineDisabled);
                    int.TryParse(values[1], out int toolOversize);
                    
                    tool.MagazineLocationDisabled = magazineDisabled == 1;
                    tool.ToolOversize = toolOversize == 1;
                }
            }
        }
        
        protected override void LoadIntData(string sectionKey, ITurningTool tool)
        {
            string offsetIntStr = _reader.ReadString(sectionKey, _config.OffsetDataIntKey, "");
            if (!string.IsNullOrEmpty(offsetIntStr))
            {
                var values = offsetIntStr.Split(',');
                if (values.Length >= 2)
                {
                    int.TryParse(values[0], out int spindleDir);
                    int.TryParse(values[1], out int holderDir);
                    
                    tool.SpindleDirection = (SpindleDirection)spindleDir;
                    tool.HolderDirection = (HolderDirection)holderDir;
                }
            }
            
            string wearDataStr = _reader.ReadString(sectionKey, _config.WearDataIntKey, "");
            if (!string.IsNullOrEmpty(wearDataStr))
            {
                var values = wearDataStr.Split(',');
                if (values.Length >= _config.WearDataBoolValueCount)
                {
                    int.TryParse(values[0], out int toolDisabled);
                    int.TryParse(values[1], out int tcwParam);
                    
                    tool.ToolDisabled = toolDisabled == 1;
                    tool.TcwParameter = (TCWParameter)tcwParam;
                }
            }
        }
        
        protected override void LoadEdgesData(string sectionKey, int edgesCount, ITurningTool tool)
        {
            for (int edge = 1; edge <= edgesCount; edge++)
            {
                LoadOffsetEdgeData(sectionKey, edge, tool);
                LoadWearEdgeData(sectionKey, edge, tool);
            }
        }
        
        private void LoadOffsetEdgeData(string sectionKey, int edge, ITurningTool tool)
        {
            string dataStr = _reader.ReadString(sectionKey, _config.GetOffsetEdgeFloatKey(edge), "");
            
            if (string.IsNullOrEmpty(dataStr)) return;
            if (!tool.OffsetEdgeData.TryGetValue(edge, out var offsetEdge)) return;
            
            var values = dataStr.Split(',');
            if (values.Length >= _config.OffsetEdgeFloatValueCount)
            {
                float.TryParse(values[0], out float lengthX);
                float.TryParse(values[1], out float lengthZ);
                float.TryParse(values[2], out float radius);
                float.TryParse(values[3], out float holderAngle);
                int.TryParse(values[4], out int insertAngle);
                float.TryParse(values[5], out float insertLength);
                
                offsetEdge.LengthX = lengthX;
                offsetEdge.LengthZ = lengthZ;
                offsetEdge.Radius = radius;
                offsetEdge.HolderAngle = holderAngle;
                offsetEdge.InsertAngle = insertAngle;
                offsetEdge.InsertLength = insertLength;
            }
        }
        
        private void LoadWearEdgeData(string sectionKey, int edge, ITurningTool tool)
        {
            string dataStr = _reader.ReadString(sectionKey, _config.GetWearEdgeFloatKey(edge), "");
            
            if (string.IsNullOrEmpty(dataStr)) return;
            if (!tool.WearEdgeData.TryGetValue(edge, out var wearEdge)) return;
            
            var values = dataStr.Split(',');
            if (values.Length >= _config.WearEdgeFloatValueCount)
            {
                float.TryParse(values[0], out float wearLengthX);
                float.TryParse(values[1], out float wearLengthZ);
                float.TryParse(values[2], out float wearRadius);
                float.TryParse(values[3], out float toolLife);
                float.TryParse(values[4], out float toolNominalLife);
                float.TryParse(values[5], out float toolLimitLife);
                float.TryParse(values[6], out float toolQuantity);
                float.TryParse(values[7], out float toolNominalQuantity);
                float.TryParse(values[8], out float toolLimitQuantity);
                float.TryParse(values[9], out float toolWear);
                float.TryParse(values[10], out float toolNominalWear);
                float.TryParse(values[11], out float toolLimitWear);
                
                wearEdge.WearLengthX = wearLengthX;
                wearEdge.WearLengthZ = wearLengthZ;
                wearEdge.WearRadius = wearRadius;
                wearEdge.ToolLife = toolLife;
                wearEdge.ToolNominalLife = toolNominalLife;
                wearEdge.ToolLimitLife = toolLimitLife;
                wearEdge.ToolQuantity = toolQuantity;
                wearEdge.ToolNominalQuantity = toolNominalQuantity;
                wearEdge.ToolLimitQuantity = toolLimitQuantity;
                wearEdge.ToolWear = toolWear;
                wearEdge.ToolNominalWear = toolNominalWear;
                wearEdge.ToolLimitWear = toolLimitWear;
            }
        }
    }
}