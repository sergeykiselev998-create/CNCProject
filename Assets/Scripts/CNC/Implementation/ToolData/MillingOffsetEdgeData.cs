using System;
using CNC.Interfaces.Tool.MillingData;
using UnityEngine;

namespace CNC.Implementation.ToolData
{
    [Serializable]
    public class MillingOffsetEdgeData : IMillingOffsetEdgeData
    {
        [field: SerializeField] public float Length { get; set; }
        [field: SerializeField] public float Diameter { get; set; }
        [field: SerializeField] public float TipAngle { get; set; }
        [field: SerializeField] public int NumberOfTeeth { get; set; }

        public MillingOffsetEdgeData(float length = 0f, float diameter = 0f, float tipAngle = 0f, int numberOfTeeth = 0)
        {
            Length = length;
            Diameter = diameter;
            TipAngle = tipAngle;
            NumberOfTeeth = numberOfTeeth;
        }
    }
}