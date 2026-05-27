using System;
using System.Collections.Generic;
using CNC.Implementation.ToolData;
using CNC.Interfaces.Tool;

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
        public List<TurningOffsetEdgeData> Offsets = new (); // Индекс 0 соответствует Edge 1


        public static SerializableTurningTool FromInterface(ITurningTool mainData)
        {
            var serializable = new SerializableTurningTool
            {
                Id = mainData.Id,
                ToolName = mainData.ToolName,
                Offsets = new List<TurningOffsetEdgeData>()
            };

            if (mainData.OffsetEdgeData != null)
            {
                var sortedKeys = new List<int>(mainData.OffsetEdgeData.Keys);
                sortedKeys.Sort();
                foreach (var key in sortedKeys)
                {
                    if (mainData.OffsetEdgeData[key] is TurningOffsetEdgeData offsetImpl)
                        serializable.Offsets.Add(offsetImpl);
                }
            }

            return serializable;
        }
    }
}