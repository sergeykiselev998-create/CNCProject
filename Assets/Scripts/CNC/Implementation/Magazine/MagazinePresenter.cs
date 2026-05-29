using CNC.Interfaces.Events;
using CNC.Interfaces.Magazine;
using CNC.Interfaces.Tool;
using Reflex.Attributes;
using UnityEngine;

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

        public void Load(int location, int toolId)
        {
            if(!Model.TryLoad(location, toolId))
            {
                Debug.Log($"[{this.GetType().Name}] Load failed in location: {location} toolId: {toolId}");
                return;
            }
            
            if(!Model.TryGetTool(location, out var tool))
            {
                Debug.Log($"[{this.GetType().Name}] Tool not found with toolId: {toolId}");
                return;
            }
            
            View.UnloadMagazine(location, tool);
        }

        public void Unload(int location)
        {
            if (!Model.TryUnload(location))
            {
                Debug.Log($"[{this.GetType().Name}] Slot location {location} is already unloaded");
                return;
            }

            View.UnloadMagazine(location, Model.GetEmptyTool());
        }

        public void Dispose()
        {
        }
    }
}