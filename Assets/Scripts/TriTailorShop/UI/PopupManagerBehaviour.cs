using UnityEngine;

namespace TriTailorShop.UI
{
   public class PopupManagerBehaviour : MonoBehaviour
   {
      [SerializeField] protected PopupInventoryBehaviour popupInventory;
      [SerializeField] protected PopupShopBehaviour popupShop;
      [SerializeField] protected PopupBehaviour popupHelp;
      [SerializeField] protected PopupConfirmQuitBehaviour popupConfirmQuit;
      
      public static PopupManagerBehaviour Instance { get; private set; }

      private void Awake()
      {
         Instance = this;
      }

      public void ShowHelp()
      {
         popupHelp.gameObject.SetActive(true);
      }

      public void ShowInventory()
      {
         popupInventory.gameObject.SetActive(true);
      }

      public void ShowConfirmQuit()
      {
         popupConfirmQuit.gameObject.SetActive(true);
      }

      public void ShowShop()
      {
         popupShop.gameObject.SetActive(true);
      }
   }
}
