using System;
using CNC.Enums;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Views;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CNC.Implementation.Slots
{
    public class MainSlot : BaseSlotHandler, ISlotView<ITool>
    {
        [SerializeField] private TMP_Text m_Location;
        [SerializeField] private TMP_Text m_Edge;
        [SerializeField] private TMP_InputField m_ToolName;
        [SerializeField] private Image m_LastInteractIcon;
        [SerializeField] private Image m_SpindleIcon;

        public UnityEvent onNameChanged = new();

        private void OnEnable()
        {
            m_ToolName.onEndEdit.AddListener(OnNameChange);
        }

        private void OnDisable()
        {
            m_ToolName.onEndEdit.RemoveListener(OnNameChange);
        }

        private void OnNameChange(string value)
        {
            if (m_ToolName.text != value)
                onNameChanged?.Invoke();
        }

        public void SetSiblingIndex(int siblingIndex)
        {
            transform.SetSiblingIndex(siblingIndex);
        }
        
        public void ApplyState(SlotState state, SlotContext context)
        {
            bool isSpindle = context == SlotContext.Spindle;

            m_SpindleIcon.gameObject.SetActive(isSpindle);
            m_LastInteractIcon.gameObject.SetActive(isSpindle);
            
            switch (state)
            {
                case SlotState.Load:
                    Load(m_Location, m_Edge, m_ToolName);
                    break;

                case SlotState.Unload:
                    Unload(m_Edge, m_ToolName);
                    break;

                case SlotState.Edge:
                    Unload(m_Location);
                    Load( m_Edge, m_ToolName);
                    DisableInteraction(m_ToolName);
                    break;
            }
        }

        public void UpdateData(int location, int edge, ITool tool)
        {
            m_Location.text = location.ToString();
            m_Edge.text = edge.ToString();
            m_ToolName.SetTextWithoutNotify(tool.ToolName);
        }

        public void ResetUI()
        {
            m_Location.text = "";
            m_Edge.text = "1";
            m_ToolName.SetTextWithoutNotify("");
        }
    }
}