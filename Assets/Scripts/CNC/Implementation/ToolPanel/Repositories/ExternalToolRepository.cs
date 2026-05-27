using System.Collections.Generic;
using CNC.Implementation.Config;
using CNC.Interfaces.Config;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.ToolPanel.Repositories
{
    public abstract class ExternalToolRepository<T> where T : IMainData
    {
        protected readonly IConfigReader _reader;
        protected readonly ExternalToolConfig _config;
    
        protected ExternalToolRepository(ExternalToolConfig config)
        {
            _config = config;
            _reader = new IniReader(config.FilePath);
        }
    
        protected abstract T Get(int id);
    
        public Dictionary<int, T> GetAll()
        {
            var tools = new Dictionary<int, T>();
        
            if (!_config.FileExists)
                return tools;
        
            int count = _reader.ReadInt(_config.MainSection, _config.CountKey, 0);
        
            for (int id = 1; id <= count; id++)
            {
                var tool = Get(id);
                if (tool != null)
                {
                    tools[id] = tool;
                }
            }
        
            return tools;
        }
    }
}