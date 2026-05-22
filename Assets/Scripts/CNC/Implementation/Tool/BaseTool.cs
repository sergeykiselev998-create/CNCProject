using System;
using System.Collections.Generic;
using CNC.Interfaces.Offsets;
using CNC.Interfaces.Tool;
using UnityEngine;

namespace CNC.Implementation.Tool
{
    /// <summary>
    /// Базовый класс инструмента
    /// </summary>
    [Serializable]
    public abstract class BaseTool : ITool
    {
        [field: SerializeField] public int Id { get; set; }
        [field: SerializeField] public string ToolName { get; set; }
        public Dictionary<int, IOffset> Offsets { get; set; }
        protected BaseTool(int id, string toolName, Dictionary<int, IOffset> offsets)
        {
            Id = id;
            ToolName = toolName;
            Offsets = offsets;
        }
    }
}
