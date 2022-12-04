using UnityEngine;

namespace TriTailorShop.UI
{
    public class PopupConfirmQuitBehaviour : PopupBehaviour
    {
        public void Quit()
        {
            Application.Quit();
        }
    }
}