using CNC.Enums;
using CNC.Interfaces.Tool.MillingData;
using CNC.Interfaces.Views;
using CNC.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CNC.Implementation.Slots
{
    public class MillingOffsetSlot : BaseSlotHandler, ISlotView<IMillingOffsetData>
    {
        [SerializeField] private TMP_InputField m_Length;
        [SerializeField] private TMP_InputField m_Diameter;
        [SerializeField] private TMP_InputField m_TipAngle;
        [SerializeField] private Toggle m_CoolantToggle;

        private int _edgeIndex;

        public event UnityAction<int, string> OnLengthChanged;
        public event UnityAction<int, string> OnDiameterChanged;
        public event UnityAction<int, string> OnTipAngleChanged;
        public event UnityAction<int, bool> OnCoolantToggled;
        
        private void OnEnable()
        {
            m_Length.onEndEdit.AddListener(HandleLengthChange);
            m_Diameter.onEndEdit.AddListener(HandleDiameterChange);
            m_TipAngle.onEndEdit.AddListener(HandleTipAngleChange);
            m_CoolantToggle.onValueChanged.AddListener(HandleCoolantToggle);
        }

        private void OnDisable()
        {
            m_Length.onEndEdit.RemoveListener(HandleLengthChange);
            m_Diameter.onEndEdit.RemoveListener(HandleDiameterChange);
            m_TipAngle.onEndEdit.RemoveListener(HandleTipAngleChange);
            m_CoolantToggle.onValueChanged.RemoveListener(HandleCoolantToggle);
        }

        private void HandleLengthChange(string value) => OnLengthChanged?.Invoke(_edgeIndex, value);
        private void HandleDiameterChange(string value) => OnDiameterChanged?.Invoke(_edgeIndex, value);
        private void HandleTipAngleChange(string value) => OnTipAngleChanged?.Invoke(_edgeIndex, value);
        private void HandleCoolantToggle(bool value) => OnCoolantToggled?.Invoke(_edgeIndex, value);

        public void ApplyState(SlotDisplayType state, SlotLocationType context)
        {
            switch (state)
            {
                case SlotDisplayType.Load:
                    EnableElement(m_Length, m_Diameter, m_TipAngle, m_CoolantToggle);
                    break;

                case SlotDisplayType.Unload:
                    DisableElement(m_Length, m_Diameter, m_TipAngle, m_CoolantToggle);
                    break;

                case SlotDisplayType.Edge:
                    EnableElement(m_Length, m_Diameter, m_TipAngle);
                    DisableInteraction(m_CoolantToggle);
                    break;
            }
        }

        public void UpdateData(int location, int edge, IMillingOffsetData tool)
        {
            _edgeIndex = edge;
            var offset = tool.OffsetEdgeData[edge];
            var length = TextFormatter.Format(offset.Length);
            var diameter = TextFormatter.Format(offset.Diameter);
            
            SetLengthWithoutNotify(length);
            SetDiameterWithoutNotify(diameter);
        }

        public void SetSiblingIndex(int siblingIndex) => transform.SetSiblingIndex(siblingIndex);
        
        public void SetLengthWithoutNotify(string value) => m_Length.SetTextWithoutNotify(value);

        public void SetDiameterWithoutNotify(string value) => m_Diameter.SetTextWithoutNotify(value);

        public void SetTipAngleWithoutNotify(string value) => m_TipAngle.SetTextWithoutNotify(value);

        public void SetIsCoolantEnabledWithoutNotify(bool value) => m_CoolantToggle.SetIsOnWithoutNotify(value);
        
        public void ResetUI()
        {
            const string def = "0.000";
            m_Length.SetTextWithoutNotify(def);
            m_Diameter.SetTextWithoutNotify(def);
            m_TipAngle.SetTextWithoutNotify(def);
        }
    }
}