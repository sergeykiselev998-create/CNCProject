using UnityEngine;
using UnityEngine.UI;

namespace CNC.Interfaces.Views
{
    public interface ISlotHandler
    {
        void EnableInteraction(Selectable element);
        void DisableInteraction(Selectable element);
        void Load(Behaviour element);
        void Unload(Behaviour element);
    
        void EnableInteraction(params Selectable[] elements);
        void DisableInteraction(params Selectable[] elements);
        void Load(params Behaviour[] elements);
        void Unload(params Behaviour[] elements);
    }
}