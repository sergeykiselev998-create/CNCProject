using System;
using CNC.Interfaces.Offsets;
using UnityEngine;

namespace CNC.Implementation.Offsets
{
    /// <summary>
    /// Implementation of tool offset
    /// </summary>
    [Serializable]
    public class Offset : IOffset
    {
        [field: SerializeField] public float X { get; set; }
        [field: SerializeField] public float Y { get; set; }
        [field: SerializeField] public float Z { get; set; }
        [field: SerializeField] public float T { get; set; }

        public Offset() : this(0, 0, 0, 0) { }

        public Offset(float x, float y, float z, float t)
        {
            X = x;
            Y = y;
            Z = z;
            T = t;
        }

        public void Set(float x, float y, float z, float t)
        {
            X = x;
            Y = y;
            Z = z;
            T = t;
        }
    }
}