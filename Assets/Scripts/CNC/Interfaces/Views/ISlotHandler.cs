using UnityEngine;
using UnityEngine.UI;

namespace CNC.Interfaces.Views
{
    public interface ISlotHandler
    {
        void EnableInteraction(Selectable element);
        void DisableInteraction(Selectable element);
        void EnableElement(Behaviour element);
        void Unload(Behaviour element);
    
        void EnableInteraction(params Selectable[] elements);
        void DisableInteraction(params Selectable[] elements);
        void EnableElement(params Behaviour[] elements);
        void DisableElement(params Behaviour[] elements);
    }
}