using System.Collections.Generic;
using System.IO;
using CNC.Implementation.Config;
using CNC.Interfaces.Config;
using CNC.Interfaces.Offsets;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolList;
using UnityEngine;

namespace CNC.Implementation.ToolList
{
    public abstract class ToolRepository<T> : IToolRepository<T> where T : ITool
    {
        protected readonly Dictionary<int, T> _tools = new();
        protected readonly IConfigReader _reader;
        protected readonly ToolConfig _config;
        
        public IOffsetRepository<T> Offsets { get; protected set; }
        public Dictionary<int, T> Tools => _tools;

        protected ToolRepository(ToolConfig config, IOffsetRepository<T> offsetRepository)
        {
            _config = config;
            _reader = new IniReader(config.FilePath);
            Offsets = offsetRepository;
        }

        public void Load()
        {
            Clear();

            if (!_config.FileExists)
                return;

            int count = _reader.ReadInt(_config.MainSection, _config.CountKey, 0);
            
            for (int id = 1; id <= count; id++)
            {
                var tool = CreateTool(id);
                if (tool != null)
                {
                    _tools[id] = tool;
                }
            }
        }
        
        protected abstract T CreateTool(int id);
        public abstract T CreateEmptyTool();
        
        public virtual bool TryGetTool(int id, out T? tool)
        {
            return _tools.TryGetValue(id, out tool);
        }

        public virtual void Add(T tool)
        {
            _tools[tool.Id] = tool;
        }

        public virtual void Remove(int id)
        {
            _tools.Remove(id);
        }

        protected virtual void Clear()
        {
            _tools.Clear();
        }
    }
}