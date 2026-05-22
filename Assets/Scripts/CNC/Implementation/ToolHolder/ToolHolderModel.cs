using System;
using System.Collections.Generic;
using CNC.Implementation.Offsets;
using CNC.Implementation.Tool;
using CNC.Interfaces.Offsets;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolHolder;
using CNC.Interfaces.ToolList;

namespace CNC.Implementation.ToolHolder
{
    public class ToolHolderModel<TTool> : IToolHolderModel<TTool> 
        where TTool : ITool
    {
        public TTool CurrentTool { get; private set; }
        public int CurrentLocation { get; private set; } = -1;
        public int CurrentToolId { get; private set; } = -1;
        public bool HasTool => CurrentTool != null;
        
        private IToolRepository<TTool> _repository { get; }

        public event Action<TTool> OnToolChanged;
        public event Action OnToolUnloaded;
        
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