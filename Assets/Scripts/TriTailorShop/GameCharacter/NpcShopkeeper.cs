using TriTailorShop.Data;

namespace TriTailorShop.GameCharacter
{
    public class NpcShopkeeper : GameCharacter
    {
        public const float BUY_PRICE_RATE = 0.5f;
        public const float SELL_PRICE_RATE = 1f;

        private float m_buyPriceRate = BUY_PRICE_RATE;
        private float m_sellPriceRate = SELL_PRICE_RATE;

        public static NpcShopkeeper Instance { get; set; }

        public void LoadFromAsset(PlayerDataAsset asset)
        {
            Inventory.SetMaxSlot(asset.maxSlot);

            for (int a = 0; a < asset.itemIds.Count; a++)
            {
                var item = new ItemData(asset.itemIds[a]);
                inventory.AddItem(item);
            }
        }

       public bool Sell(Player player, ItemData item, out string error)
        {
            if (player.Inventory.IsFull && item.masterData.type != ItemType.Material)
            {
                error = "error.inventory_full";
                return false;
            }
            
            // find item in shopkeeper inventory
            var index = this.Inventory.FindItemIndex(item.instanceId);
            if (index < 0)
            {
                error = "error.item_not_found";
                return false;
            }
            
            int price = (int)(item.masterData.shopPrice * m_sellPriceRate);
            
            int qty = item.quantity;

            int totalPrice = qty * price;
            
            if (!player.ResourceGold.TrySpend(totalPrice))
            {
                error = "error.not_enough_gold";
                return false;
            }

            error = "";
            Inventory.RemoveItem(index);

            // if item is material pack, then unpack and add to resource instead
            if (item.masterData.type == ItemType.Material)
            {
                ResourceData res = player.GetResourceData(item.masterData.id);
                res.Add(item.quantity);
            }
            else
            {
                player.Inventory.AddItem(item);
            }
            
            return true;
        }

        public bool Buy(Player player, ItemData item)
        {
            // find item in player inventory
            var index = player.Inventory.FindItemIndex(item.instanceId);
            if (index < 0) return false;

            int price = (int)(item.masterData.shopPrice * m_buyPriceRate);
            
            int qty = item.quantity;

            int totalPrice = qty * price;

            if (inventory.AddItem(item))
            {
                player.ResourceGold.Add(totalPrice);
                player.Inventory.RemoveItem(index);
                return true;
            }
            
            return false;
        }
    }
}