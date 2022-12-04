using System;
using System.Collections.Generic;
using TriTailorShop.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TriTailorShop.UI
{
    public enum PriceDisplayMode
    {
        None = 0,
        Buying = 1,
        Selling = 2
    }

    public class InventoryUiBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected ItemSlotUiBehaviour slotUiPrefab;
        
        [SerializeField]
        protected Transform contentTransform;

        private Inventory m_inventory;
        private bool m_dirty;
        private List<ItemSlotUiBehaviour> m_itemSlotUIs = new List<ItemSlotUiBehaviour>();
        private PriceDisplayMode m_priceMode;
        
        public UnityEvent<ItemSlotUiBehaviour> onItemSelected = new UnityEvent<ItemSlotUiBehaviour>();

        public void PairInventory(Inventory inventory)
        {
            // remove event trigger from old inventory, if any
            if (m_inventory != null)
            {
                m_inventory.onInventoryChanged.RemoveListener(OnInventoryChanged);
            }

            m_inventory = inventory;
            m_inventory.onInventoryChanged.AddListener(OnInventoryChanged);
            m_dirty = true;

            RebuildSlots();
        }

        private void OnInventoryChanged()
        {
            m_dirty = true;
        }
        
        // Get last slot that contain the item, mostly to play animation
        public ItemSlotUiBehaviour GetLastItemSlot()
        {
            for (int a = 0; a < m_itemSlotUIs.Count; a++)
            {
                if (m_itemSlotUIs[a].CurrentItem == null)
                {
                    if (a == 0) return null;

                    return m_itemSlotUIs[a - 1];
                }
            }

            return null;
        }

        public void Update()
        {
            // this avoid refresh shop UI multiple times if inventory change a lot in logic
            if (m_dirty)
            {
                RefreshUi();
                m_dirty = false;
            }
        }

        private void RebuildSlots()
        {
            if (m_inventory.MaxSlot > m_itemSlotUIs.Count)
            {
                for (int a = m_itemSlotUIs.Count; a < m_inventory.MaxSlot; a++)
                {
                    ItemSlotUiBehaviour slotUI = Instantiate(slotUiPrefab, contentTransform, false);
                    slotUI.onRightMouseClicked.AddListener(OnItemRightClicked);
                    m_itemSlotUIs.Add(slotUI);
                }
            }
            else if (m_inventory.MaxSlot < m_itemSlotUIs.Count)
            {
                while (m_itemSlotUIs.Count > m_inventory.MaxSlot)
                {
                    var lastIndex = m_itemSlotUIs.Count - 1;
                    var removedSlotUI = m_itemSlotUIs[lastIndex];
                    removedSlotUI.transform.SetParent(null);
                    Destroy(removedSlotUI);
                }
            }
        }

        private void OnItemRightClicked(ItemSlotUiBehaviour slotUI)
        {
            onItemSelected.Invoke(slotUI);
        }

        private void RefreshUi()
        {
            for (int a = 0; a < m_inventory.MaxSlot; a++)
            {
                ItemData item = m_inventory.GetItemAt(a);
                m_itemSlotUIs[a].SetItem(item);
                m_itemSlotUIs[a].SetPriceMode(m_priceMode);
            }
        }

        public void SetPriceMode(PriceDisplayMode priceMode)
        {
            m_priceMode = priceMode;
            foreach (var itemUI in m_itemSlotUIs)
            {
                itemUI.SetPriceMode(priceMode);
            }
        }
    }
}
