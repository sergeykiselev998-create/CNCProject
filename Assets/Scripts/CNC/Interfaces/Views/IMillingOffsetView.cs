using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Views
{
    public interface IMillingOffsetView : ISlotView<IMillingTool>
    {
        string Length { get; set; }
        string Diameter { get; set; }
        string TipAngle { get; set; }

        bool IsCoolantEnabled { get; set; }

        UnityEngine.Events.UnityEvent OnLengthChanged { get; }
        UnityEngine.Events.UnityEvent OnDiameterChanged { get; }
        UnityEngine.Events.UnityEvent OnTipAngleChanged { get; }
        UnityEngine.Events.UnityEvent OnCoolantToggled { get; }
    }
}