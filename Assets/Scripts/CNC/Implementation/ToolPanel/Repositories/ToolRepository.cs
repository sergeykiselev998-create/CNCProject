using System.Collections.Generic;
using System.Linq;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolPanel;

namespace CNC.Implementation.ToolPanel.Repositories
{
    public abstract class ToolRepository<T> : IToolRepository<T> where T : IMainData
    {
        private readonly ExternalToolRepository<T> _externalRepository;
        private readonly AdditionalToolRepository<T> _additionalRepository;
        private Dictionary<int, T> _tools = new();
    
        public Dictionary<int, T> Tools => _tools;
    
        protected ToolRepository(ExternalToolRepository<T> externalRepository, AdditionalToolRepository<T> additionalRepository)
        {
            _externalRepository = externalRepository;
            _additionalRepository = additionalRepository;
        }

        public abstract T CreateEmptyTool();

        public void Load()
        {
            _tools = _externalRepository.GetAll();
    
            foreach (var id in _tools.Keys.ToList())
            {
                var tool = _tools[id];
                _additionalRepository.TryLoad(id, ref tool);
                _tools[id] = tool;
            }
        }
    
        public void SaveAdditional()
        {
            _additionalRepository.SaveAll(_tools);
        }
    
        public bool TryGetTool(int id, out T tool)
        {
            return _tools.TryGetValue(id, out tool);
        }
        

        public void AddTool(T tool)
        {
            _tools[tool.Id] = tool;
        }
    
        public void RemoveTool(int id)
        {
            _tools.Remove(id);
        }
    }
}