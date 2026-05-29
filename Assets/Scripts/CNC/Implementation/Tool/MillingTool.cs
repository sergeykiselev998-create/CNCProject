using System;
using System.Collections.Generic;
using System.Linq;
using CNC.Enums;
using CNC.Implementation.ToolData;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Tool.MillingData;
using UnityEngine;

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
        public SortedDictionary<int, IMillingOffsetEdgeData> OffsetEdgeData { get; }

        //Wear
        public bool ToolDisabled { get; set; }
        public TCWParameter TcwParameter { get; set; }
        public SortedDictionary<int, IMillingWearEdgeData> WearEdgeData { get; }
        
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

            OffsetEdgeData = new SortedDictionary<int, IMillingOffsetEdgeData>
            {
                [1] = new MillingOffsetEdgeData(0, diameter)
            };
            
            WearEdgeData = new SortedDictionary<int, IMillingWearEdgeData>
            {
                [1] = new MillingWearEdgeData()
            };
        }

        public void AddEdge(int triggeredEdgeIndex)
        {
            if (!OffsetEdgeData.ContainsKey(triggeredEdgeIndex))
            {
                Debug.Log($"[{this.GetType().Name}] Edge {triggeredEdgeIndex} not found");
                return;
            }

            int newEdgeIndex = -1;
            int maxExistingIndex = OffsetEdgeData.Keys.Max();
    
            for (int i = triggeredEdgeIndex + 1; i <= maxExistingIndex + 1; i++)
            {
                if (!OffsetEdgeData.ContainsKey(i))
                {
                    newEdgeIndex = i;
                    break;
                }
            }

            if (newEdgeIndex == -1)
            {
                Debug.Log($"[{this.GetType().Name}] Failed to find a free index for new edge");
                return;
            }
            
            OffsetEdgeData[newEdgeIndex] = OffsetEdgeData[triggeredEdgeIndex].Clone();
            WearEdgeData[newEdgeIndex] = WearEdgeData[triggeredEdgeIndex].Clone();
        }
    }
}
