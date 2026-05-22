using CNC.Implementation.Controls;
using CNC.Implementation.Slots;
using UnityEngine;
using UnityEngine.Serialization;

namespace CNC.Implementation.Config
{
    [CreateAssetMenu(fileName = "ToolPanelConfig", menuName = "CNC/Tool Panel Config")]
    public class ToolPanelConfig : ScriptableObject
    {
        [Header("Common Controls")] 
        public MainSlotControl MainSlotControl;
        public ThirdSlotControl ThirdSlotControl;
        public FourthSlotControl FourthSlotControl;

        [Header("Offset Controls")] 
        public TurningOffsetSlotControl TurningOffsetSlotControl;
        public MillingOffsetSlotControl MillingOffsetSlotControl;
        
        [Header("Common Slots")]
        public MainSlot MainSlotPrefab;
        public ThirdSlot ThirdSlotPrefab;
        public FourthSlot FourthSlotPrefab;

        [Header("Offset Slots")]
        public TurningOffsetSlot TurningOffsetPrefab;
        public MillingOffsetSlot MillingOffsetPrefab;
    }
}