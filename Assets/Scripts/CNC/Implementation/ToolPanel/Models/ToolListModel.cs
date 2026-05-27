using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolPanel;
using UnityEngine;
using UnityEngine.Events;

namespace CNC.Implementation.ToolPanel.Models
{
    public abstract class ToolListModel<TTool> : IToolListModel<TTool> 
        where TTool : IMainData
    {
        public event UnityAction<int, string> OnToolNameChanged;
        
        private readonly IToolRepository<TTool> _repository;

        protected ToolListModel(IToolRepository<TTool> repository)
        {
            _repository = repository;
        }

        public void UpdateToolName(int id, string newName)
        {
            if (!TryGetTool(id, out var tool))
                return;

            tool.ToolName = newName;
            OnToolNameChanged?.Invoke(id, newName);
        }
        
        public bool TryGetTool(int id, out TTool tool)
        {
            var result = _repository.TryGetTool(id, out tool);
            
            if(!result)
                Debug.Log($"[{GetType().Name}] Tool not found by id: {id}");
            
            return result;
        }
    }
}