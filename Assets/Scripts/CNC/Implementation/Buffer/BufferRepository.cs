using System.Collections.Generic;
using System.Linq;
using CNC.Interfaces.Buffer;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolList;
using UnityEngine;

namespace CNC.Implementation.Buffer
{
    /// <summary>
    /// Repository implementation for buffer slots (dynamic list with -1 marker at the end)
    /// </summary>
    public class BufferRepository<T> : IBufferRepository<T> where T : ITool
    {
        [field: SerializeField] public IToolRepository<T> ToolRepository { get; set; }
        [field: SerializeField] public List<int> Slots { get; private set; }
        
        private const int EMPTY_MARKER = -1;

        public BufferRepository(IToolRepository<T> toolRepository)
        {
            ToolRepository = toolRepository;
            Slots = new List<int> { EMPTY_MARKER };
        }

        public void Add(int toolId)
        {
            Slots.Insert(Slots.Count - 1, toolId);
        }

        public void Remove(int toolId)
        {
            if (toolId == EMPTY_MARKER) return;
            
            int index = Slots.IndexOf(toolId);
            if (index != -1)
            {
                Slots.RemoveAt(index);
            }
        }

        public bool TryGetTool(int toolId, out T tool)
        {
            return ToolRepository.TryGetTool(toolId, out tool);
        }

        public void SetTools(IEnumerable<int> bufferTools)
        {
            Slots = new List<int>(bufferTools) { EMPTY_MARKER };
        }
        
        public List<int> GetTools()
        {
            return Slots.Take(Slots.Count - 1).ToList();
        }

        public T GetEmptyTool()
        {
            return ToolRepository.CreateEmptyTool();
        }
        
        public bool Contains(int toolId)
        {
            return toolId != EMPTY_MARKER && Slots.Contains(toolId);
        }
    }
}