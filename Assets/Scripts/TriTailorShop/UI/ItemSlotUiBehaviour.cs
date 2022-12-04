using TMPro;
using TriTailorShop.Data;
using TriTailorShop.GameCharacter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TriTailorShop.UI
{
    public class ItemSlotUiBehaviour : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        protected Image imgIcon;
        
        [SerializeField]
        protected TextMeshProUGUI txtPrice;

        [SerializeField] 
        protected TextMeshProUGUI txtQuantity;
        
        private PriceDisplayMode m_priceMode;
        private bool m_dirty;
        private Animator m_animator;
      
        // Start is called before the first frame update
        private ItemData m_item;

        public readonly UnityEvent<ItemSlotUiBehaviour> onRightMouseClicked = new UnityEvent<ItemSlotUiBehaviour>();

        public ItemData CurrentItem => m_item;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        // set null mean empty item
        public void SetItem(ItemData item)
        {
            m_item = item;
            m_dirty = true;
        }
        
        public void SetPriceMode(PriceDisplayMode mode)
        {
            m_priceMode = mode;
            m_dirty = true;
        }
        
        void RefrehUI()
        {
            // empty slot
            if (m_item == null)
            {
                imgIcon.gameObject.SetActive(false);
                txtPrice.gameObject.SetActive(false);
                txtQuantity.gameObject.SetActive(false);
            }
            else
            {
                imgIcon.gameObject.SetActive(true);
                imgIcon.sprite = m_item.masterData.icon;

                switch (m_priceMode)
                {
                    case PriceDisplayMode.None: 
                        txtPrice.gameObject.SetActive(false);
                        break;
                    
                    case PriceDisplayMode.Buying: 
                        txtPrice.gameObject.SetActive(true);
                        SetPriceText(NpcShopkeeper.SELL_PRICE_RATE);
                        break;
                    
                    case PriceDisplayMode.Selling:
                        txtPrice.gameObject.SetActive(true);
                        SetPriceText(NpcShopkeeper.BUY_PRICE_RATE);
                        break;
                }

                if (m_item.quantity > 1)
                {
                    txtQuantity.gameObject.SetActive(true);
                    txtQuantity.text = "x" + m_item.quantity;
                }
                else
                {
                    txtQuantity.gameObject.SetActive(false);
                }
            }
        }

        void SetPriceText(float rate)
        {
            var price = m_item.masterData.shopPrice;
            var finalPrice = (int) (m_item.quantity * price * rate);
            txtPrice.text = finalPrice + "";
        }

        // trigger bounce animation
        public void Bounce()
        {
            m_animator.SetTrigger("Bounce");
        }

        public void FlashRed()
        {
            m_animator.SetTrigger("FlashRed");
        }

        void Update()
        {
            if (m_dirty)
            {
                RefrehUI();
                m_dirty = false;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                onRightMouseClicked.Invoke(this);
            }
        }
    }
}
