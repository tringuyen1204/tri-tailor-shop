using System;
using System.Collections;
using TriTailorShop.Data;
using TriTailorShop.GameCharacter;
using UnityEngine;

namespace TriTailorShop.UI
{
    public class PopupShopBehaviour : PopupBehaviour
    {
        [SerializeField]
        protected InventoryUiBehaviour inventoryUiPlayer;
        
        [SerializeField]
        protected InventoryUiBehaviour inventoryUiShopkeeper;

        private NpcShopkeeper m_npcShopkeeper;

        private void Start()
        {
            m_npcShopkeeper = NpcShopkeeper.Instance;

            // setup player inventory
            inventoryUiPlayer.SetPriceMode(PriceDisplayMode.Selling);
            inventoryUiPlayer.PairInventory(Player.Main.Inventory);
            inventoryUiPlayer.onItemSelected.AddListener( SellItem );
            
            // setup shop keeper inventory
            inventoryUiShopkeeper.SetPriceMode(PriceDisplayMode.Buying);
            inventoryUiShopkeeper.PairInventory(NpcShopkeeper.Instance.Inventory);
            inventoryUiShopkeeper.onItemSelected.AddListener( BuyItem);
        }

        private void BuyItem(ItemSlotUiBehaviour slotUI)
        {
            ItemData item = slotUI.CurrentItem;
            
            if (item == null)
            {
                return;
            }

            if (m_npcShopkeeper.Sell(Player.Main, item, out string error))
            {
                if (item.masterData.type != ItemType.Material)
                {
                    StartCoroutine(BounceLastItem(inventoryUiPlayer));
                }
               
            }
            else
            {
                if (error == "error.not_enough_gold")
                {
                    slotUI.FlashRed();
                }
                else if (error == "error.inventory_full")
                {
                    HudController.ShowNotification("Inventory is full!", Color.red);;
                }
            }
        }
        
        private IEnumerator BounceLastItem(InventoryUiBehaviour inventory)
        {
            yield return new WaitForSeconds(0.05f);
            var lastSlot = inventory.GetLastItemSlot();

            if (lastSlot)
            {
                lastSlot.Bounce();
            }
        }

        private void SellItem(ItemSlotUiBehaviour slotUI)
        {
            if (slotUI.CurrentItem == null)
            {
                return;
            }

            if (m_npcShopkeeper.Buy(Player.Main, slotUI.CurrentItem))
            {
                StartCoroutine(BounceLastItem(inventoryUiShopkeeper));
            }
        }
    }
}