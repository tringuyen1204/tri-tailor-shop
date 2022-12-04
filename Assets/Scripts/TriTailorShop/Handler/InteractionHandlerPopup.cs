using TriTailorShop.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace TriTailorShop.InteractionHandler
{
    public class InteractionHandlerPopup : InteractionHandler
    {
        [FormerlySerializedAs("popupShop")] public PopupBehaviour popup;
    
        private void Interact()
        {
            popup.gameObject.SetActive(true);
        }

        protected virtual  void Update()
        {
            if (!inParameter) return;
            if (PopupBehaviour.UiHandlingInput) return;
            
            if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Return))
            {
                Interact();
            }
        }
    }
}