using CNC.Enums;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;
using UnityEngine;

namespace CNC.Implementation.Slots
{
    public class ThirdSlot : BaseSlotHandler, ISlotView<IMainData>
    {
        public void ApplyState(SlotDisplayType state, SlotLocationType context)
        {
        }

        public void UpdateData(int location, int edge, IMainData mainData)
        {
        }

        public void SetSiblingIndex(int siblingIndex)
        {
            transform.SetSiblingIndex(siblingIndex);
        }

        public void ResetUI()
        {
        }
    }
}