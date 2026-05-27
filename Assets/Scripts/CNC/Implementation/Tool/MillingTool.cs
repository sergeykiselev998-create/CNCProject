using System;
using System.Collections.Generic;
using System.Linq;
using CNC.Enums;
using CNC.Implementation.ToolData;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Tool.MillingData;

namespace CNC.Implementation.Tool
{
    /// <summary>
    /// Фрезерный инструмент
    /// </summary>
    [Serializable]
    public class MillingTool : IMillingTool
    {
        //Main
        public int Id { get; set; }
        public string ToolName { get; set; }
        public int CutterType { get; set; }

        //Offset
        public bool Coolant1 { get;set;  }
        public bool Coolant2 { get; set; }
        public SpindleDirection SpindleDirection { get; set; }
        public Dictionary<int, IMillingOffsetEdgeData> OffsetEdgeData { get; }

        //Wear
        public bool ToolDisabled { get; set; }
        public TCWParameter TcwParameter { get; set; }
        public Dictionary<int, IMillingWearEdgeData> WearEdgeData { get; }
        
        //Magazine
        public bool MagazineLocationDisabled { get; set; }
        public bool ToolOversize { get; set; }
        public bool ToolOnFixedLocation { get; set; }

        public int CountEdges => OffsetEdgeData.Count;
        public int[] GetEdges => OffsetEdgeData.Keys.ToArray();
        
        public MillingTool(int id, string toolName, float diameter, int cutterType)
        {
            Id = id;
            ToolName = toolName;
            CutterType = cutterType;

            OffsetEdgeData = new Dictionary<int, IMillingOffsetEdgeData>
            {
                [1] = new MillingOffsetEdgeData(0, diameter)
            };
            
            WearEdgeData = new Dictionary<int, IMillingWearEdgeData>
            {
                [1] = new MillingWearEdgeData()
            };
        }
    }
}
