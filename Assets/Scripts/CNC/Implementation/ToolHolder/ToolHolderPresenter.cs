using CNC.Interfaces.Events;
using CNC.Interfaces.Strategies;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolHolder;
using Reflex.Attributes;

namespace CNC.Implementation.ToolHolder
{
    public class ToolHolderPresenter<TTool>: IToolHolderPresenter<TTool>
    where TTool : IMainData
    {
        public IToolHolderModel<TTool> Model { get; }
        public IToolHolderView<TTool> View { get; }
        public IToolHolderStrategy Strategy { get; }
        
        [Inject]
        public IEventBus EventBus { get; }

        public ToolHolderPresenter(IToolHolderModel<TTool> model, IToolHolderView<TTool> view, IToolHolderStrategy strategy)
        {
            Model = model;
            View = view;
            Strategy = strategy;
        }

        public void Initialize()
        {
            if (!Strategy.IsUIAllowed)
                return;
            if (!Model.TryGetTool(out var tool))
            {
                tool = Model.GetEmptyTool();
            }
            View.AddToolHolder(-1, tool);
        }
        public void Dispose()
        {
        }
    }
}