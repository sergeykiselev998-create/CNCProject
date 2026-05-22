using CNC.Enums;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;
using CNC.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CNC.Implementation.Slots
{
    public class MillingOffsetSlot : BaseSlotHandler, IMillingOffsetView
    {
        [SerializeField] private TMP_InputField m_Length;
        [SerializeField] private TMP_InputField m_Diameter;
        [SerializeField] private TMP_InputField m_TipAngle;
        [SerializeField] private Toggle m_CoolantToggle;

        public UnityEvent onLengthChanged = new();
        public UnityEvent onDiameterChanged = new();
        public UnityEvent onTipAngleChanged = new();
        public UnityEvent onCoolantToggled = new();

        public UnityEvent OnLengthChanged => onLengthChanged;
        public UnityEvent OnDiameterChanged => onDiameterChanged;
        public UnityEvent OnTipAngleChanged => onTipAngleChanged;
        public UnityEvent OnCoolantToggled => onCoolantToggled;

        public string Length
        {
            get => m_Length.text;
            set => m_Length.SetTextWithoutNotify(value);
        }
        
        public string Diameter
        {
            get => m_Diameter.text;
            set => m_Diameter.SetTextWithoutNotify(value);
        }
        
        public string TipAngle
        {
            get => m_TipAngle.text;
            set => m_TipAngle.SetTextWithoutNotify(value);
        }

        public bool IsCoolantEnabled
        {
            get => m_CoolantToggle.isOn;
            set => m_CoolantToggle.SetIsOnWithoutNotify(value);
        }
        
        private void OnEnable()
        {
            m_Length.onEndEdit.AddListener(_ => onLengthChanged?.Invoke());
            m_Diameter.onEndEdit.AddListener(_ => onDiameterChanged?.Invoke());
            m_TipAngle.onEndEdit.AddListener(_ => onTipAngleChanged?.Invoke());
            m_CoolantToggle.onValueChanged.AddListener(_ => onCoolantToggled?.Invoke());
        }

        private void OnDisable()
        {
            m_Length.onEndEdit.RemoveListener(_ => onLengthChanged?.Invoke());
            m_Diameter.onEndEdit.RemoveListener(_ => onDiameterChanged?.Invoke());
            m_TipAngle.onEndEdit.RemoveListener(_ => onTipAngleChanged?.Invoke());
            m_CoolantToggle.onValueChanged.RemoveListener(_ => onCoolantToggled?.Invoke());
        }

        public void ApplyState(SlotState state, SlotContext context)
        {
            switch (state)
            {
                case SlotState.Load:
                    Load(m_Length, m_Diameter, m_TipAngle, m_CoolantToggle);
                    break;

                case SlotState.Unload:
                    Unload(m_Length, m_Diameter, m_TipAngle, m_CoolantToggle);
                    break;

                case SlotState.Edge:
                    Load(m_Length, m_Diameter, m_TipAngle);
                    DisableInteraction(m_CoolantToggle);
                    break;
            }
        }

        public void UpdateData(int location, int edge, IMillingTool tool)
        {
            var offset = tool.Offsets[edge];
            var length = TextFormatter.Format(offset.X);
            
            string diameter = diameter = TextFormatter.Format(tool.Diameter);
            
            m_Length.SetTextWithoutNotify(length);
            m_Diameter.SetTextWithoutNotify(diameter);
        }

        public void SetSiblingIndex(int siblingIndex) => transform.SetSiblingIndex(siblingIndex);
        
        public void ResetUI()
        {
            const string def = "0.000";
            m_Length.SetTextWithoutNotify(def);
            m_Diameter.SetTextWithoutNotify(def);
            m_TipAngle.SetTextWithoutNotify(def);
        }
    }
}