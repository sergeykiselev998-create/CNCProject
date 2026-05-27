using System;
using System.Collections.Generic;
using CNC.Implementation.ToolData;
using CNC.Interfaces.Tool;

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
        public List<MillingOffsetEdgeData> Offsets = new();
        

        public static SerializableMillingTool FromInterface(IMillingTool tool)
        {
            var serializable = new SerializableMillingTool
            {
                Id = tool.Id,
                ToolName = tool.ToolName,
                Offsets = new List<MillingOffsetEdgeData>()
            };

            // Конвертируем Dictionary в List по порядку ключей (1, 2, 3...)
            if (tool.OffsetEdgeData != null)
            {
                var sortedKeys = new List<int>(tool.OffsetEdgeData.Keys);
                sortedKeys.Sort();
                foreach (var key in sortedKeys)
                {
                    if (tool.OffsetEdgeData[key] is MillingOffsetEdgeData offsetImpl)
                        serializable.Offsets.Add(offsetImpl);
                }
            }

            return serializable;
        }
    }
}