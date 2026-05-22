using System;
using System.Collections.Generic;
using CNC.Implementation.Offsets;
using CNC.Interfaces.Tool;
using UnityEngine;

namespace CNC.Implementation.DebugEntryPoint
{
    /// <summary>
    /// Serializable версия TurningTool для отображения в инспекторе.
    /// </summary>
    [Serializable]
    public class SerializableTurningTool : ISerializableTool
    {
        public int Id;
        public string ToolName;
        public List<Offset> Offsets = new List<Offset>(); // Индекс 0 соответствует Edge 1

        [Header("Turning Specific")]
        public float ShiftX;
        public float ShiftY;

        public static SerializableTurningTool FromInterface(ITurningTool tool)
        {
            var serializable = new SerializableTurningTool
            {
                Id = tool.Id,
                ToolName = tool.ToolName,
                ShiftX = tool.ShiftX,
                ShiftY = tool.ShiftY,
                Offsets = new List<Offset>()
            };

            if (tool.Offsets != null)
            {
                var sortedKeys = new List<int>(tool.Offsets.Keys);
                sortedKeys.Sort();
                foreach (var key in sortedKeys)
                {
                    if (tool.Offsets[key] is Offset offsetImpl)
                        serializable.Offsets.Add(offsetImpl);
                }
            }

            return serializable;
        }
    }
}