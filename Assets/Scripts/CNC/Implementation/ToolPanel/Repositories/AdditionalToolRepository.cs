using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CNC.Implementation.Config;
using CNC.Interfaces.Config;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.ToolPanel.Repositories
{
    public abstract class AdditionalToolRepository<TTool> where TTool : IMainData
    {
        protected readonly IConfigReader _reader;
        protected readonly AdditionalToolConfig _config;
        
        protected AdditionalToolRepository(AdditionalToolConfig config)
        {
            _config = config;
            EnsureFileIntegrity();
            _reader = new IniReader(_config.FilePath);
        }
        
        private void EnsureFileIntegrity()
        {
            string tempFilePath = _config.FilePath + ".tmp";
            
            if (File.Exists(tempFilePath))
            {
                if (File.Exists(_config.FilePath))
                    File.Delete(_config.FilePath);
                
                File.Move(tempFilePath, _config.FilePath);
            }
        }
        
        public bool TryLoad(int id, ref TTool tool)
        {
            if (!_config.FileExists)
                return false;
            
            var sectionKey = $"{_config.ToolSectionPrefix}{id}";
            
            if (!_reader.SectionExists(sectionKey))
                return false;
            
            int edgesCount = _reader.ReadInt(sectionKey, _config.EdgesCountKey, 0);
            if (edgesCount == 0)
                return false;
            
            LoadBoolData(sectionKey, tool);
            LoadIntData(sectionKey, tool);
            LoadEdgesData(sectionKey, edgesCount, tool);
            
            return true;
        }
        
        public void SaveAll(Dictionary<int, TTool> tools)
        {
            string tempFilePath = _config.FilePath + ".tmp";
            
            var tempWriter = new IniWriter(tempFilePath);
            tempWriter.WriteInt(_config.MainSection, _config.CountKey, tools.Count);
            
            foreach (var tool in tools.Values)
            {
                SaveDataToWriter(tool.Id, tool, tempWriter);
            }
            
            if (File.Exists(_config.FilePath))
                File.Delete(_config.FilePath);
            
            File.Move(tempFilePath, _config.FilePath);
        }
        
        protected string ToInvariant(float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
        
        protected abstract void SaveDataToWriter(int id, TTool tool, IConfigWriter writer);
        protected abstract void LoadBoolData(string sectionKey, TTool tool);
        protected abstract void LoadIntData(string sectionKey, TTool tool);
        protected abstract void LoadEdgesData(string sectionKey, int edgesCount, TTool tool);
    }
}