using System;
using System.Collections.Generic;
using CNC.Interfaces.Offsets;
using CNC.Interfaces.Tool;
using UnityEngine;

namespace CNC.Implementation.Tool
{
    /// <summary>
    /// Токарный инструмент
    /// </summary>
    [Serializable]
    public class TurningTool : BaseTool, ITurningTool
    {
        [field: SerializeField] public float ShiftX { get; set; }
        [field: SerializeField] public float ShiftY { get; set; }

        public TurningTool(int id, string toolName, Dictionary<int, IOffset> offsets, float shiftX, float shiftY)
            : base(id, toolName, offsets)
        {
            ShiftX = shiftX;
            ShiftY = shiftY;
        }
    }
}
