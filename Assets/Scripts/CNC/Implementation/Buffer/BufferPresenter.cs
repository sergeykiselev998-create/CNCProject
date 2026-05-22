using CNC.Interfaces.Buffer;
using CNC.Interfaces.Events;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolList;
using Reflex.Attributes;

namespace CNC.Implementation.Buffer
{
    public class BufferPresenter<TTool> : IBufferPresenter<TTool>
    where TTool : ITool
    {
        public IBufferModel<TTool> Model { get; }
        public IBufferView<TTool> View { get; }
        
        [Inject]
        public IEventBus EventBus { get; }

        public BufferPresenter(IBufferModel<TTool> model, IBufferView<TTool> view)
        {
            Model = model;
            View = view;
        }

        public void Initialize()
        {
            var emptyTool = Model.GetEmptyTool();
            View.AddBuffer(-1, emptyTool);
            
            foreach (var id in Model.GetTools())
            {
                if (Model.TryGetTool(id, out var tool))
                    View.AddBuffer(-1, tool);
            }
        }
        
        public void Dispose()
        {
            
        }
    }
}