using TriTailorShop.Data;
using UnityEngine.Events;

namespace TriTailorShop.GameCharacter
{
    public class Player : GameCharacter
    {
        public ResourceData ResourceGold { get; }  = new ResourceData("gold");
        public ResourceData ResourceLeather { get; } = new ResourceData("leather");
        public ResourceData ResourceFur { get; } = new ResourceData("fur");
        
        public ResourceData ResourceCotton { get; } = new ResourceData("cotton");
        
        public ResourceData ResourceSilk { get; } = new ResourceData("silk");

        private ItemData m_helmet;
        private ItemData m_cloth;

        public ItemData EquippedHelmet => m_helmet;
        public ItemData EquippedCloth => m_cloth;
        
        public Player()
        {
            
        }
        
        public UnityEvent onHelmetChanged = new UnityEvent();
        public UnityEvent onClothChanged = new UnityEvent();
        
        public static Player Main { get; set; }

        // load default data from a scriptable object
        public void LoadFromAsset(PlayerDataAsset asset)
        {
            ResourceGold.Add(asset.gold);
            ResourceFur.Add(asset.fur);
            ResourceLeather.Add(asset.leather);
            ResourceCotton.Add(asset.cotton);
            ResourceSilk.Add(asset.silk);
            
            Inventory.SetMaxSlot(asset.maxSlot);

            for (int a = 0; a < asset.itemIds.Count; a++)
            {
                var item = new ItemData(asset.itemIds[a]);
                inventory.AddItem(item);
            }
        }

        public ResourceData GetResourceData(string id)
        {
            switch (id)
            {
                case "gold" : return ResourceGold;
                case "fur" : return ResourceFur;
                case "leather" : return ResourceLeather;
                case "cotton" : return ResourceCotton;
                case "silk" : return ResourceSilk;
            }

            return null;
        }
        
        // equip null item is to remove this item
        public bool EquipHelmet(ItemData item, out ItemData unequippedItem)
        {
            if (item != null)
            {
                unequippedItem = m_helmet;
                m_helmet = item;
                Inventory.SwapItem(unequippedItem, item);
            }
            else // equip null item is to remove this item from player
            {
                unequippedItem = m_helmet;
                m_helmet = null;
                Inventory.AddItem(unequippedItem);
                
            }
            
            onHelmetChanged.Invoke();
            return true;
        }
        
        
        public bool EquipCloth(ItemData item, out ItemData unequippedItem)
        {
            if (item != null)
            {
                unequippedItem = m_cloth;
                m_cloth = item;
                Inventory.SwapItem(unequippedItem, item);
            }
            else   // equip null item is to remove this item from player
            {
                unequippedItem = m_cloth;
                m_cloth = null;
                Inventory.AddItem(unequippedItem);
            }

            onClothChanged.Invoke();
            return true;
        }
    }
}