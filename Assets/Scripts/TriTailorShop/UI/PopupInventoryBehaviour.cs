using System.Collections;
using TriTailorShop.Data;
using TriTailorShop.GameCharacter;
using TriTailorShop.Spine;
using UnityEngine;

namespace TriTailorShop.UI
{
    public class PopupInventoryBehaviour : PopupBehaviour
    {
        [SerializeField]
        protected InventoryUiBehaviour inventoryUiBehaviour;

        [SerializeField] 
        protected SpineRegionChangerBehaviour spineRegionChanger;

        [SerializeField]
        protected ItemSlotUiBehaviour slotHelmet;
        
        [SerializeField]
        protected ItemSlotUiBehaviour slotCloth;

        private Player m_player;

        private void Awake()
        {
            m_player = Player.Main;
        }

        private void Start()
        {
            // pair inventory data with inventory UI
            inventoryUiBehaviour.PairInventory(Player.Main.Inventory);
            inventoryUiBehaviour.onItemSelected.AddListener(OnItemSelected);
            
            // listen the event that player change equipment to update UI
            m_player.onHelmetChanged.AddListener(OnHelmetChanged);
            m_player.onClothChanged.AddListener(OnClothChanged);
            
            // listen the event mouse click to remove item from equipped slot
            slotHelmet.onRightMouseClicked.AddListener(OnRemoveHelmet);
            slotCloth.onRightMouseClicked.AddListener(OnRemoveCloth);
        }

        private void OnRemoveHelmet(ItemSlotUiBehaviour slotUI)
        {
            if (slotUI.CurrentItem != null)
            {
                Player.Main.EquipHelmet(null, out ItemData unequippedItem);
                slotUI.SetItem(null);
            }

            StartCoroutine(BounceLastItem());
        }
        
        private void OnRemoveCloth(ItemSlotUiBehaviour slotUI)
        {
            if (slotUI.CurrentItem != null)
            {
                Player.Main.EquipCloth(null, out ItemData unequippedItem);
                slotUI.SetItem(null);
            }
            
            StartCoroutine(BounceLastItem());
        }
        
        private IEnumerator BounceLastItem()
        {
            yield return new WaitForSeconds(0.05f);
            var lastSlot = inventoryUiBehaviour.GetLastItemSlot();
            lastSlot.Bounce();
        }

        private void OnHelmetChanged()
        {
            var helmet = m_player.EquippedHelmet;
            if (helmet != null)
            {
                spineRegionChanger.EquipEquipment(ItemType.Helmet, helmet.masterData.id);
                slotHelmet.Bounce();
            }
            else
            {
                spineRegionChanger.EquipEquipment(ItemType.Helmet, "helmet_default");
            }
        }
        
        private void OnClothChanged()
        {
            var cloth = m_player.EquippedCloth;
            if (cloth != null)
            {
                spineRegionChanger.EquipEquipment(ItemType.Cloth, cloth.masterData.id);
                slotCloth.Bounce();
            }
            else
            {
                spineRegionChanger.EquipEquipment(ItemType.Cloth, "cloth_default");
            }   
        }

        private void OnItemSelected(ItemSlotUiBehaviour slot)
        {
            ItemData item = slot.CurrentItem;
            
            // click empty slot
            if (item == null)
            {
                return;
            }

            if (item.masterData.type == ItemType.Helmet)
            {
                if (Player.Main.EquipHelmet(item, out ItemData unequippedItem))
                {
                    slotHelmet.SetItem(item);

                    if (unequippedItem != null)
                    {
                        slot.SetItem(unequippedItem);
                        slot.Bounce();
                    }
                }
            }
            else if (item.masterData.type == ItemType.Cloth)
            {
                if (Player.Main.EquipCloth(item, out ItemData unequippedItem))
                {
                    slotCloth.SetItem(item);

                    if (unequippedItem != null)
                    {
                        slot.SetItem(unequippedItem);
                        slot.Bounce();
                    }
                }
            }
            
          
        }
    }
}