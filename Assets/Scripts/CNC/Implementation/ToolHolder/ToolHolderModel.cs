using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolHolder;
using CNC.Interfaces.ToolPanel;
using UnityEngine.Events;

namespace CNC.Implementation.ToolHolder
{
    public class ToolHolderModel<TTool> : IToolHolderModel<TTool> 
        where TTool : IMainData
    {
        public TTool CurrentTool { get; private set; }
        public int CurrentLocation { get; private set; } = -1;
        public int CurrentToolId { get; private set; } = -1;
        public bool HasTool => CurrentTool != null;
        
        private IToolRepository<TTool> _repository { get; }

        public event UnityAction<TTool> OnToolChanged;
        public event UnityAction OnToolUnloaded;
        
        public ToolHolderModel(IToolRepository<TTool> repository)
        {
            _repository = repository;
        }

        public bool TryLoadTool(TTool tool, int location)
        {
            if (tool == null)
                return false;

            CurrentTool = tool;
            CurrentLocation = location;
            CurrentToolId = tool.Id;

            OnToolChanged?.Invoke(CurrentTool);
            return true;
        }

        public void UnloadTool()
        {
            if (!HasTool)
                return;

            CurrentTool = default;
            CurrentLocation = -1;
            CurrentToolId = -1;

            OnToolUnloaded?.Invoke();
        }

        public bool TryGetTool(out TTool tool)
        {
            tool = CurrentTool;
            return HasTool;
        }
        
        public TTool GetEmptyTool()
        {
            return _repository.CreateEmptyTool();
        }
    }
}