namespace CNC.Implementation.Config
{
    public class AdditionalToolConfig : BaseConfig
    {
        public string ToolSectionPrefix { get; set; } = "T";
        public string EdgesCountKey { get; set; } = "EdgesCount";
    
        // Типы данных
        public string BoolPrefix { get; set; } = "Bool:";
        public string IntPrefix { get; set; } = "Int:";
        public string FloatPrefix { get; set; } = "Float:";
    
        // Группы параметров с указанием количества значений
        public string OffsetDataKey { get; set; } = "OffsetData";
        public int OffsetDataValueCount { get; set; } = 0;  // Coolant1, Coolant2
    
        public string MagazineDataKey { get; set; } = "MagazineData";
        public int MagazineDataBoolValueCount { get; set; } = 0;  // MagazineDisabled, ToolOversize, FixedLocation
    
        public string WearDataKey { get; set; } = "WearData";
        public int WearDataBoolValueCount { get; set; } = 0;  // ToolDisabled, TcwParameter (как int)
    
        // Параметры для кромок
        public string OffsetEdgeKey { get; set; } = "OffsetEdge";
        public int OffsetEdgeFloatValueCount { get; set; } = 0;  // Length, Diameter, TipAngle (все float)
    
        public string WearEdgeKey { get; set; } = "WearEdge";
        public int WearEdgeFloatValueCount { get; set; } = 0;  // Все float значения WearEdge
    
        // Int параметры для кромок (отдельно, так как это Int)
        public string OffsetEdgeIntKey { get; set; } = "OffsetEdgeInt";
        public int OffsetEdgeIntValueCount { get; set; } = 0;  // NumberOfTeeth
        
        public string OffsetDataBoolKey => $"{BoolPrefix}{OffsetDataKey}";
        public string MagazineDataBoolKey => $"{BoolPrefix}{MagazineDataKey}";
        public string OffsetDataIntKey => $"{IntPrefix}{OffsetDataKey}";
        public string WearDataIntKey => $"{IntPrefix}{WearDataKey}";
    
        public string GetOffsetEdgeFloatKey(int edge) => $"{FloatPrefix}{OffsetEdgeKey}{edge}";
        public string GetOffsetEdgeIntKey(int edge) => $"{IntPrefix}{OffsetEdgeKey}{edge}";
        public string GetWearEdgeFloatKey(int edge) => $"{FloatPrefix}{WearEdgeKey}{edge}";
    }
}