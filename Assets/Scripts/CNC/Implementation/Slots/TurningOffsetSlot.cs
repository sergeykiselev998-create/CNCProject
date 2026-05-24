using CNC.Enums;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;
using CNC.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CNC.Implementation.Slots
{
    public class TurningOffsetSlot : BaseSlotHandler, ITurningOffsetView
    {
        [SerializeField] private TMP_InputField m_LengthX;
        [SerializeField] private TMP_InputField m_LengthZ;
        [SerializeField] private TMP_InputField m_Radius;
        [SerializeField] private TMP_Dropdown m_ToolTypeDropdown;

        public UnityEvent OnLengthXChanged { get; } = new();
        public UnityEvent OnLengthZChanged { get; } = new();
        public UnityEvent OnRadiusChanged { get; } = new();
        public UnityEvent OnToolTypeChanged { get; } = new();

        public string LengthX
        {
            get => m_LengthX.text;
            set => m_LengthX.SetTextWithoutNotify(value);
        }

        public string LengthZ
        {
            get => m_LengthZ.text;
            set => m_LengthZ.SetTextWithoutNotify(value);
        }

        public string Radius
        {
            get => m_Radius.text;
            set => m_Radius.SetTextWithoutNotify(value);
        }

        public int SelectedToolTypeIndex
        {
            get => m_ToolTypeDropdown.value;
            set => m_ToolTypeDropdown.SetValueWithoutNotify(value);
        }

        public void SetToolTypeOptions(string[] options)
        {
            m_ToolTypeDropdown.ClearOptions();
            m_ToolTypeDropdown.AddOptions(new System.Collections.Generic.List<string>(options));
        }

        private void OnEnable()
        {
            m_LengthX.onEndEdit.AddListener(_ => OnLengthXChanged.Invoke());
            m_LengthZ.onEndEdit.AddListener(_ => OnLengthZChanged.Invoke());
            m_Radius.onEndEdit.AddListener(_ => OnRadiusChanged.Invoke());
            m_ToolTypeDropdown.onValueChanged.AddListener(_ => OnToolTypeChanged.Invoke());
        }

        private void OnDisable()
        {
            m_LengthX.onEndEdit.RemoveAllListeners();
            m_LengthZ.onEndEdit.RemoveAllListeners();
            m_Radius.onEndEdit.RemoveAllListeners();
            m_ToolTypeDropdown.onValueChanged.RemoveAllListeners();
        }

        public void ApplyState(SlotDisplayType state, SlotLocationType context)
        {
            switch (state)
            {
                case SlotDisplayType.Load:
                    Load(m_LengthX, m_LengthZ, m_Radius, m_ToolTypeDropdown);
                    break;

                case SlotDisplayType.Unload:
                    Unload(m_LengthX, m_LengthZ, m_Radius, m_ToolTypeDropdown);
                    break;

                case SlotDisplayType.Edge:
                    Load(m_LengthX, m_LengthZ, m_Radius);
                    DisableInteraction(m_ToolTypeDropdown);
                    break;
            }
        }

        public void UpdateData(int location, int edge, ITurningTool tool)
        {
            var offset = tool.Offsets[edge];
            m_LengthX.SetTextWithoutNotify(TextFormatter.Format(offset.X));
            m_LengthZ.SetTextWithoutNotify(TextFormatter.Format(offset.Z));
            m_Radius.SetTextWithoutNotify(TextFormatter.Format(tool.ShiftX));
        }

        public void ResetUI()
        {
            const string def = "0.000";
            m_LengthX.SetTextWithoutNotify(def);
            m_LengthZ.SetTextWithoutNotify(def);
            m_Radius.SetTextWithoutNotify(def);
        }

        public void SetSiblingIndex(int siblingIndex) => transform.SetSiblingIndex(siblingIndex);
    }
}