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
    public class MainSlot : BaseSlotHandler, ISlotView<IMainData>
    {
        [SerializeField] private TMP_Text m_Location;
        [SerializeField] private TMP_Text m_Edge;
        [SerializeField] private TMP_InputField m_ToolName;
        [SerializeField] private Image m_LastInteractIcon;
        [SerializeField] private Image m_SpindleIcon;

        private int _currentEdge;
        
        public event UnityAction<string> OnNameChanged;
        public event UnityAction<int> OnAddEdge;
        public event UnityAction<int> OnRemoveEdge;

        private void OnEnable()
        {
            m_ToolName.onEndEdit.AddListener(HandleNameChange);
        }

        private void OnDisable()
        {
            m_ToolName.onEndEdit.RemoveListener(HandleNameChange);
        }

        public void HandleAddEdge()
        {
            OnAddEdge?.Invoke(_currentEdge);
        }
        
        public void HandleRemoveEdge()
        {
            OnRemoveEdge?.Invoke(_currentEdge);
        }
        
        private void HandleNameChange(string value)
        {
            OnNameChanged?.Invoke(value);
        }

        public void SetSiblingIndex(int siblingIndex)
        {
            transform.SetSiblingIndex(siblingIndex);
        }
        
        public void ApplyState(SlotDisplayType state, SlotLocationType context)
        {
            bool isSpindle = context == SlotLocationType.Spindle;

            m_SpindleIcon.gameObject.SetActive(isSpindle);
            m_LastInteractIcon.gameObject.SetActive(isSpindle);
            
            switch (state)
            {
                case SlotDisplayType.Load:
                    EnableElement(m_Location, m_Edge, m_ToolName);
                    break;

                case SlotDisplayType.Unload:
                    DisableElement(m_Edge, m_ToolName);
                    break;

                case SlotDisplayType.Edge:
                    Unload(m_Location);
                    EnableElement( m_Edge, m_ToolName);
                    DisableInteraction(m_ToolName);
                    break;
            }
        }

        public void UpdateData(int location, int edge, IMainData mainData)
        {
            _currentEdge = edge;
            
            m_Location.text = location.ToString();
            m_Edge.text = edge.ToString();
            SetNameWithoutNotify(mainData.ToolName);
        }

        public void SetNameWithoutNotify(string newName)
        {
            m_ToolName.SetTextWithoutNotify(newName);
        }

        public void ResetUI()
        {
            m_Location.text = "";
            m_Edge.text = "1";
            m_ToolName.SetTextWithoutNotify("");
        }
    }
}