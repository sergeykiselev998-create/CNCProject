using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Views
{
    public interface ITurningOffsetView : ISlotView<ITurningTool>
    {
        string LengthX { get; set; }
        string LengthZ { get; set; }
        string Radius { get; set; }

        void SetToolTypeOptions(string[] options);
        int SelectedToolTypeIndex { get; set; }

        UnityEngine.Events.UnityEvent OnLengthXChanged { get; }
        UnityEngine.Events.UnityEvent OnLengthZChanged { get; }
        UnityEngine.Events.UnityEvent OnRadiusChanged { get; }
        UnityEngine.Events.UnityEvent OnToolTypeChanged { get; }
    }
}