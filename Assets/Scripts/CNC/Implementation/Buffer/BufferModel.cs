using System;
using System.Collections.Generic;
using CNC.Interfaces.Buffer;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.Buffer
{
    public class BufferModel<T> : IBufferModel<T> where T : IMainData
    {
        private readonly IBufferRepository<T> _repository;
        
        public BufferModel(IBufferRepository<T> repository)
        {
            _repository = repository;
        }
        
        public bool TryAddTool(int toolId)
        {
            if (_repository.Contains(toolId))
                return false;
            
            _repository.Add(toolId);
            return true;
        }
        
        public bool TryRemoveTool(int toolId)
        {
            if (!_repository.Contains(toolId)) 
                return false;
    
            _repository.Remove(toolId);
            return true;
        }

        public bool TryGetTool(int toolId, out T tool)
        {
            return _repository.TryGetTool(toolId, out tool);
        }

        public List<int> GetTools()
        {
            return _repository.GetTools();
        }
        
        public T GetEmptyTool()
        {
            return _repository.GetEmptyTool();
        }
        
        public bool Contains(int toolId)
        {
            return _repository.Contains(toolId);
        }
    }
}