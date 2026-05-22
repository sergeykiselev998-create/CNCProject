using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolList;

namespace CNC.Implementation.ToolList
{
    public class ToolListModel<T> : IToolListModel<T> where T : ITool
    {
        private readonly IToolRepository<T> _repository;
        
        public ToolListModel(IToolRepository<T> repository)
        {
            _repository = repository;
        }
    }
}