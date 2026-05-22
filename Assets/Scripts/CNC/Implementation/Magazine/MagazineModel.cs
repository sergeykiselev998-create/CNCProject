using System.Collections.Generic;
using CNC.Interfaces.Magazine;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.Magazine
{
    public class MagazineModel<T> : IMagazineModel<T> where T : ITool
    {
        private readonly IMagazineRepository<T> _repository;
        
        //public IMagazineRepository<T> Repository => _repository;

        public MagazineModel(IMagazineRepository<T> repository)
        {
            _repository = repository;
        }

        public List<int> GetLocations()
        {
            return _repository.GetSlotsLocations();
        }

        public bool TryLoad(int location, int toolId)
        {
            if (!IsSlotFree(location))
                return false;
            
            _repository.Load(location, toolId);
            return true;
        }
        
        public bool TryUnload(int location)
        {
            if (IsSlotFree(location))
                return false;
            
            _repository.Unload(location);
            return true;
        }
        
        public bool IsSlotFree(int location)
        {
            return _repository.TryGetToolId(location, out int toolId) && toolId == -1;
        }
        
        public bool TryGetToolId(int location, out int toolId)
        {
            return _repository.TryGetToolId(location, out toolId);
        }
        
        public bool TryGetTool(int location, out T tool)
        {
            return _repository.TryGetTool(location, out tool);
        }

        public T GetEmptyTool()
        {
            return _repository.GetEmptyTool();
        }
    }
}