using System.Collections.Generic;
using System.Linq;
using CNC.Interfaces.Buffer;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolPanel;
using UnityEngine;

namespace CNC.Implementation.Buffer
{
    /// <summary>
    /// Repository implementation for buffer slots (dynamic list with -1 marker at the end)
    /// </summary>
    public class BufferRepository<T> : IBufferRepository<T> where T : IMainData
    {
        [field: SerializeField] public IToolRepository<T> ToolRepository { get; }
        [field: SerializeField] public HashSet<int> Slots { get; private set; }
        
        private const int EMPTY_MARKER = -1;

        public BufferRepository(IToolRepository<T> toolRepository)
        {
            ToolRepository = toolRepository;
            Slots = new HashSet<int> { EMPTY_MARKER };
        }

        public void Add(int toolId)
        {
            Slots.Add(toolId);
        }

        public void Remove(int toolId)
        {
            if (toolId == EMPTY_MARKER) 
                return;
            
            Slots.Remove(toolId);
        }

        public bool TryGetTool(int toolId, out T tool)
        {
            return ToolRepository.TryGetTool(toolId, out tool);
        }

        public void SetTools(IEnumerable<int> bufferTools)
        {
            Slots = new HashSet<int>(bufferTools) { EMPTY_MARKER };
        }
        
        public List<int> GetTools()
        {
            return Slots.Take(Slots.Count - 1).ToList();
        }

        public T GetEmptyTool()
        {
            return ToolRepository.EmptyTool;
        }
        
        public bool Contains(int toolId)
        {
            return toolId != EMPTY_MARKER && Slots.Contains(toolId);
        }
    }
}