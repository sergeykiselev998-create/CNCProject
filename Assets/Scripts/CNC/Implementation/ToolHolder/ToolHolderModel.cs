using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolHolder;
using CNC.Interfaces.ToolPanel;

namespace CNC.Implementation.ToolHolder
{
    public class ToolHolderModel<TTool> : IToolHolderModel<TTool> 
        where TTool : IMainData
    {
        public TTool CurrentTool { get; private set; }
        public int CurrentLocation { get; private set; } = -1;
        public int CurrentToolId { get; private set; } = -1;
        public bool HasTool => CurrentTool.Id != GetEmptyTool().Id;
        
        private readonly IToolRepository<TTool> _repository;

        public ToolHolderModel(IToolRepository<TTool> repository)
        {
            _repository = repository;
        }

        public bool TryLoadTool(int location, int toolId, out TTool tool)
        {
            if (_repository.TryGetTool(toolId, out tool))
                return false;

            CurrentTool = tool;
            CurrentLocation = location;
            CurrentToolId = tool.Id;

            return true;
        }

        public void UnloadTool()
        {
            if (!HasTool)
                return;

            CurrentTool = GetEmptyTool();
            CurrentLocation = -1;
            CurrentToolId = -1;
        }

        public bool TryGetTool(out TTool tool)
        {
            tool = CurrentTool;
            return HasTool;
        }
        
        public TTool GetEmptyTool()
        {
            return _repository.EmptyTool;
        }
    }
}