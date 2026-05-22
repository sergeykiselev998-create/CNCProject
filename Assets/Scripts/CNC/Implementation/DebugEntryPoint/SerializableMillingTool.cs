using System;
using System.Collections.Generic;
using CNC.Implementation.Offsets;
using CNC.Interfaces.Tool;
using UnityEngine;

namespace CNC.Implementation.DebugEntryPoint
{
    /// <summary>
    /// Serializable версия MillingTool для отображения в инспекторе.
    /// Использует List вместо Dictionary для нативной поддержки Unity Inspector.
    /// </summary>
    [Serializable]
    public class SerializableMillingTool : ISerializableTool
    {
        public int Id;
        public string ToolName;
        public List<Offset> Offsets = new List<Offset>(); // Индекс 0 соответствует Edge 1
        
        [Header("Milling Specific")]
        public float Diameter;
        public int CutterType;

        public static SerializableMillingTool FromInterface(IMillingTool tool)
        {
            var serializable = new SerializableMillingTool
            {
                Id = tool.Id,
                ToolName = tool.ToolName,
                Diameter = tool.Diameter,
                CutterType = tool.CutterType,
                Offsets = new List<Offset>()
            };

            // Конвертируем Dictionary в List по порядку ключей (1, 2, 3...)
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