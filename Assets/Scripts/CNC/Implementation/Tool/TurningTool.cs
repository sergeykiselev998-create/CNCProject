using System;
using System.Collections.Generic;
using System.Linq;
using CNC.Enums;
using CNC.Implementation.ToolData;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Tool.TurningData;

namespace CNC.Implementation.Tool
{
    /// <summary>
    /// Токарный инструмент
    /// </summary>
    [Serializable]
    public class TurningTool : ITurningTool
    {
        //Main
        public int Id { get; }
        public int CutterType { get; }
        public string ToolName { get; set; }
  
        //Offset
        public bool Coolant1 { get; set; }
        public bool Coolant2 { get; set; }
        public SpindleDirection SpindleDirection { get; set; }
        public HolderDirection HolderDirection { get; set; }
        public Dictionary<int, ITurningOffsetEdgeData> OffsetEdgeData { get; }
        
        //Wear
        public bool ToolDisabled { get; set; }
        public TCWParameter TcwParameter { get; set; }
        public Dictionary<int, ITurningWearEdgeData> WearEdgeData { get; }
        
        //Magazine
        public bool MagazineLocationDisabled { get; set; }
        public bool ToolOversize { get; set; }

        public int CountEdges => OffsetEdgeData.Count;
        public int[] GetEdges => OffsetEdgeData.Keys.ToArray();
        
        public TurningTool(int id, string toolName, int cutterType)
        {
            Id = id;
            ToolName = toolName;
            CutterType = cutterType;
     
            
            OffsetEdgeData = new Dictionary<int, ITurningOffsetEdgeData>
            {
                [1] = new TurningOffsetEdgeData()
            };
            
            WearEdgeData = new Dictionary<int, ITurningWearEdgeData>
            {
                [1] = new TurningWearEdgeData()
            };
        }



    }
}
