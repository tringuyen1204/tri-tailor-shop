using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TriTailorShop.UI
{
    public class PopupBehaviour : MonoBehaviour
    {
        private static readonly List<PopupBehaviour> PopupStacks = new List<PopupBehaviour>();
        
        public static bool UiHandlingInput { get; private set; }
        
        public static readonly UnityEvent<int> OnPopupAvailableChanged = new UnityEvent<int>();
     
        private void OnEnable()
        {
            PopupStacks.Add(this);
            UiHandlingInput = true;
            
            OnPopupAvailableChanged.Invoke(PopupStacks.Count);
        }

        private void OnDisable()
        {
            PopupStacks.Remove(this);
            UiHandlingInput = PopupStacks.Count > 0;
            
            OnPopupAvailableChanged.Invoke(PopupStacks.Count);
        }
        
        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}