using CNC.Interfaces.Views;
using UnityEngine;
using UnityEngine.UI;

namespace CNC.Implementation.Slots
{
    public abstract class BaseSlotHandler : MonoBehaviour, ISlotHandler
    {
        public virtual void EnableInteraction(Selectable element)
        {
            element.interactable = true;
        }

        public virtual void DisableInteraction(Selectable element)
        {
            element.interactable = false;
        }

        public virtual void Load(Behaviour element)
        {
            element.enabled = true;
        }

        public virtual void Unload(Behaviour element)
        {
            element.enabled = false;
        }

        public virtual void DisableInteraction(params Selectable[] elements)
        {
            foreach (var element in elements)
                DisableInteraction(element);
        }

        public virtual void EnableInteraction(params Selectable[] elements)
        {
            foreach (var element in elements)
                EnableInteraction(element);
        }

        public virtual void Load(params Behaviour[] elements)
        {
            foreach (var element in elements)
                Load(element);
        }

        public virtual void Unload(params Behaviour[] elements)
        {
            foreach (var element in elements)
                Unload(element);
        }
    }
}