using TriTailorShop.UI;
using UnityEngine;

namespace TriTailorShop.InteractionHandler
{
    public abstract class InteractionHandler : MonoBehaviour
    {
        [SerializeField]
        protected GameObject hintUI;
        protected bool inParameter;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                hintUI.gameObject.SetActive(true);
                inParameter = true;
            }
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                hintUI.gameObject.SetActive(false);
                inParameter = false;
            }
        }
    }
}