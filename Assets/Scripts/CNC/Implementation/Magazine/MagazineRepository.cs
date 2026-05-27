using System.Collections.Generic;
using System.Linq;
using CNC.Implementation.Config;
using CNC.Interfaces.Config;
using CNC.Interfaces.Magazine;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolPanel;
using UnityEngine;

namespace CNC.Implementation.Magazine
{
    /// <summary>
    /// Repository implementation for magazine slots (always 20 slots)
    /// </summary>
    public class MagazineRepository<T> : IMagazineRepository<T> where T : IMainData
    {
        private const int MAGAZINE_CAPACITY = 20;
        private const int EMPTY_SLOT = -1;
        private readonly IConfigReader _magazineConfig;
        private readonly MagazineConfig _config;
        [field: SerializeField] public IToolRepository<T> ToolRepository { get; }
        [field: SerializeField] public Dictionary<int, int> Slots { get; } = new(); // (Location, ToolId), -1 = empty

        public MagazineRepository(IToolRepository<T> toolRepository, MagazineConfig config)
        {
            ToolRepository = toolRepository;
            _config = config;
            _magazineConfig = new IniReader(config.FilePath);
            
            for (int location = 1; location <= MAGAZINE_CAPACITY; location++)
            {
                Slots[location] = EMPTY_SLOT;
            }
            
            LoadFromConfig();
        }

        private void LoadFromConfig()
        {
            ClearAll();
            
            if (!_config.FileExists)
                return;
            
            int capacity = _magazineConfig.ReadInt(_config.MainSection, _config.CountKey, 0);
            
            for (int pos = 1; pos <= capacity; pos++)
            {
                string keyPos = $"{_config.PositionKeyPrefix}{pos}";
                int idx = _magazineConfig.ReadInt(_config.MainSection, keyPos, EMPTY_SLOT);
                
                if (idx < 0) continue;
                
                int toolId = idx + 1; // Индекс в ID
                Load(pos, toolId);
            }
        }
        
        public bool TryGetToolId(int location, out int toolId)
        {
            if (location >= 1 && location <= MAGAZINE_CAPACITY && Slots.ContainsKey(location))
            {
                toolId = Slots[location];
                return toolId != EMPTY_SLOT;
            }
            
            toolId = EMPTY_SLOT;
            return false;
        }

        public bool TryGetTool(int location, out T tool)
        {
            if (TryGetToolId(location, out int toolId))
            {
                return ToolRepository.TryGetTool(toolId, out tool);
            }
            
            tool = default;
            return false;
        }

        public void Load(int location, int toolId)
        {
            if (location >= 1 && location <= MAGAZINE_CAPACITY)
            {
                Slots[location] = toolId;
            }
        }

        public void Unload(int location)
        {
            if (location >= 1 && location <= MAGAZINE_CAPACITY)
            {
                Slots[location] = EMPTY_SLOT;
            }
        }

        /// <summary>
        /// Find tool location by tool ID
        /// </summary>
        public int FindToolLocation(int toolId)
        {
            for (int location = 1; location <= MAGAZINE_CAPACITY; location++)
            {
                if (Slots.ContainsKey(location) && Slots[location] == toolId)
                    return location;
            }
            return EMPTY_SLOT; // Tool not found in magazine
        }

        public List<int> GetSlotsLocations()
        {
            return Slots.Keys.ToList();
        }

        public T GetEmptyTool()
        {
            return ToolRepository.CreateEmptyTool();
        }

        /// <summary>
        /// Clear all slots
        /// </summary>
        private void ClearAll()
        {
            for (int location = 1; location <= MAGAZINE_CAPACITY; location++)
            {
                Slots[location] = EMPTY_SLOT;
            }
        }
    }
}