using CNC.Interfaces.Events;
using CNC.Interfaces.Magazine;
using CNC.Interfaces.Tool;
using Reflex.Attributes;

namespace CNC.Implementation.Magazine
{
    public class MagazinePresenter<TTool> : IMagazinePresenter<TTool>
    where TTool : IMainData
    {
        public IMagazineModel<TTool> Model { get; }
        public IMagazineView<TTool> View { get; }
        
        [Inject]
        public IEventBus EventBus { get; }

        public MagazinePresenter(IMagazineModel<TTool> model, IMagazineView<TTool> view)
        {
            Model = model;
            View = view;
        }

        public void Initialize()
        {
            foreach (var location in Model.GetLocations())
            {
                if (!Model.TryGetTool(location, out var tool))
                    tool = Model.GetEmptyTool();
                View.AddMagazine(location,tool);
            }
        }
        
        public void Dispose()
        {
           // throw new System.NotImplementedException();
        }
    }
}