using CNC.Enums;
using CNC.Implementation.Config;
using CNC.Implementation.ToolData;
using CNC.Interfaces.Config;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Tool.MillingData;
using CNC.Utils;

namespace CNC.Implementation.ToolPanel.Repositories
{
    public class MillingAdditionalToolRepository : AdditionalToolRepository<IMillingTool>
    {
        public MillingAdditionalToolRepository(AdditionalToolConfig config) : base(config) { }
        
        protected override void SaveDataToWriter(int id, IMillingTool tool, IConfigWriter writer)
        {
            var sectionKey = $"{_config.ToolSectionPrefix}{id}";
            
            writer.WriteInt(sectionKey, _config.EdgesCountKey, tool.CountEdges);
            
            string offsetBoolData = $"{ (tool.Coolant1 ? 1 : 0) },{ (tool.Coolant2 ? 1 : 0) }";
            writer.WriteString(sectionKey, _config.OffsetDataBoolKey, offsetBoolData);
            
            string magazineData = $"{ (tool.MagazineLocationDisabled ? 1 : 0) },{ (tool.ToolOversize ? 1 : 0) },{ (tool.ToolOnFixedLocation ? 1 : 0) }";
            writer.WriteString(sectionKey, _config.MagazineDataBoolKey, magazineData);
            
            writer.WriteString(sectionKey, _config.OffsetDataIntKey, ((int)tool.SpindleDirection).ToString());
            
            string wearData = $"{ (tool.ToolDisabled ? 1 : 0) },{ (int)tool.TcwParameter }";
            writer.WriteString(sectionKey, _config.WearDataIntKey, wearData);
            
            for (int edge = 1; edge <= tool.CountEdges; edge++)
            {
                if (tool.OffsetEdgeData.TryGetValue(edge, out var offsetEdge))
                {
                    string offsetEdgeFloat = $"{ToInvariant(offsetEdge.Length)},{ToInvariant(offsetEdge.Diameter)},{ToInvariant(offsetEdge.TipAngle)}";
                    writer.WriteString(sectionKey, _config.GetOffsetEdgeFloatKey(edge), offsetEdgeFloat);
                    writer.WriteString(sectionKey, _config.GetOffsetEdgeIntKey(edge), offsetEdge.NumberOfTeeth.ToString());
                }
                
                if (tool.WearEdgeData.TryGetValue(edge, out var wearEdge))
                {
                    string wearEdgeData = $"{ToInvariant(wearEdge.WearLength)}," +
                                          $"{ToInvariant(wearEdge.WearDiameter)}," +
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

        protected override void LoadBoolData(string sectionKey, IMillingTool tool)
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
                    int.TryParse(values[2], out int fixedLocation);
                    
                    tool.MagazineLocationDisabled = magazineDisabled == 1;
                    tool.ToolOversize = toolOversize == 1;
                    tool.ToolOnFixedLocation = fixedLocation == 1;
                }
            }
        }
        
        protected override void LoadIntData(string sectionKey, IMillingTool tool)
        {
            string offsetIntStr = _reader.ReadString(sectionKey, _config.OffsetDataIntKey, "");
            if (!string.IsNullOrEmpty(offsetIntStr))
            {
                var values = offsetIntStr.Split(',');
                if (values.Length >= 1)
                {
                    int.TryParse(values[0], out int spindleDir);
                    tool.SpindleDirection = (SpindleDirection)spindleDir;
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
        
        protected override void LoadEdgesData(string sectionKey, int edgesCount, IMillingTool tool)
        {
            for (int edge = 1; edge <= edgesCount; edge++)
            {
                LoadOffsetEdgeData(sectionKey, edge, tool);
                LoadWearEdgeData(sectionKey, edge, tool);
            }
        }
        
        private void LoadOffsetEdgeData(string sectionKey, int edge, IMillingTool tool)
        {
            // Парсим данные
            var floatData = ParseFloatData(sectionKey, edge);
            var intData = ParseIntData(sectionKey, edge);
            
            if (!floatData.HasValue && !intData.HasValue) 
                return;
            
            // Получаем или создаем объект
            if (!tool.OffsetEdgeData.TryGetValue(edge, out var offsetEdgeInterface))
            {
                // Edge 2+ - создаем новый
                var newEdge = new MillingOffsetEdgeData();
                if (floatData.HasValue)
                {
                    newEdge.Length = floatData.Value.length;
                    newEdge.Diameter = floatData.Value.diameter;
                    newEdge.TipAngle = floatData.Value.tipAngle;
                }
                if (intData.HasValue)
                {
                    newEdge.NumberOfTeeth = intData.Value;
                }
                tool.OffsetEdgeData[edge] = newEdge;
                return;
            }
            
            // Edge 1 - обновляем существующий
            if (!offsetEdgeInterface.TryCast(out MillingOffsetEdgeData offsetEdge)) 
                return;
            
            if (floatData.HasValue)
            {
                offsetEdge.Length = floatData.Value.length;
                offsetEdge.TipAngle = floatData.Value.tipAngle;
                // Diameter не трогаем
            }
            if (intData.HasValue)
            {
                offsetEdge.NumberOfTeeth = intData.Value;
            }
        }

        private (float length, float diameter, float tipAngle)? ParseFloatData(string sectionKey, int edge)
        {
            string dataStr = _reader.ReadString(sectionKey, _config.GetOffsetEdgeFloatKey(edge), "");
            if (string.IsNullOrEmpty(dataStr)) return null;
            
            var values = dataStr.Split(',');
            if (values.Length < _config.OffsetEdgeFloatValueCount) return null;
            
            float.TryParse(values[0], out float length);
            float.TryParse(values[1], out float diameter);
            float.TryParse(values[2], out float tipAngle);
            
            return (length, diameter, tipAngle);
        }

        private int? ParseIntData(string sectionKey, int edge)
        {
            string dataStr = _reader.ReadString(sectionKey, _config.GetOffsetEdgeIntKey(edge), "");
            if (string.IsNullOrEmpty(dataStr)) return null;
            
            var values = dataStr.Split(',');
            if (values.Length < _config.OffsetEdgeIntValueCount) return null;
            
            int.TryParse(values[0], out int numberOfTeeth);
            return numberOfTeeth;
        }
        
        private void LoadWearEdgeData(string sectionKey, int edge, IMillingTool tool)
        {
            string dataStr = _reader.ReadString(sectionKey, _config.GetWearEdgeFloatKey(edge), "");
            
            if (string.IsNullOrEmpty(dataStr)) 
                return;
            
            var values = dataStr.Split(',');
            if (values.Length < _config.WearEdgeFloatValueCount) 
                return;
            
            float.TryParse(values[0], out float wearLength);
            float.TryParse(values[1], out float wearDiameter);
            float.TryParse(values[2], out float toolLife);
            float.TryParse(values[3], out float toolNominalLife);
            float.TryParse(values[4], out float toolLimitLife);
            float.TryParse(values[5], out float toolQuantity);
            float.TryParse(values[6], out float toolNominalQuantity);
            float.TryParse(values[7], out float toolLimitQuantity);
            float.TryParse(values[8], out float toolWear);
            float.TryParse(values[9], out float toolNominalWear);
            float.TryParse(values[10], out float toolLimitWear);
            
            if (tool.WearEdgeData.TryGetValue(edge, out var wearEdge))
            {
                // WearEdge существует - обновляем все поля
                wearEdge.WearLength = wearLength;
                wearEdge.WearDiameter = wearDiameter;
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
            else
            {
                // WearEdge не существует - создаем новый
                wearEdge = new MillingWearEdgeData
                {
                    WearLength = wearLength,
                    WearDiameter = wearDiameter,
                    ToolLife = toolLife,
                    ToolNominalLife = toolNominalLife,
                    ToolLimitLife = toolLimitLife,
                    ToolQuantity = toolQuantity,
                    ToolNominalQuantity = toolNominalQuantity,
                    ToolLimitQuantity = toolLimitQuantity,
                    ToolWear = toolWear,
                    ToolNominalWear = toolNominalWear,
                    ToolLimitWear = toolLimitWear
                };
                tool.WearEdgeData[edge] = wearEdge;
            }
        }
    }
}