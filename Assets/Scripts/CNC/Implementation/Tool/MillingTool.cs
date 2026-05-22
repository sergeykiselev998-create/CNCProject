using System;
using System.Collections.Generic;
using CNC.Interfaces.Offsets;
using CNC.Interfaces.Tool;
using UnityEngine;

namespace CNC.Implementation.Tool
{
    /// <summary>
    /// Фрезерный инструмент
    /// </summary>
    [Serializable]
    public class MillingTool : BaseTool, IMillingTool
    {
        [field: SerializeField] public int CutterType { get; set; }
        [field: SerializeField] public float Diameter { get; set; }

        public MillingTool(int id, string toolName, Dictionary<int, IOffset> offsets, float diameter, int cutterType)
            : base(id, toolName, offsets)
        {
            Diameter = diameter;
            CutterType = cutterType;
        }
    }
}
