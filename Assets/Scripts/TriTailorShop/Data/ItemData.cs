using UnityEngine;

namespace TriTailorShop.Data
{
    public class ItemData
    {
        public string instanceId;
        public ItemMasterData masterData;
        public int quantity;

        public ItemData(string id)
        {
            instanceId = StringUtilities.GenerateHexId(8);
            masterData = ItemUtilities.GetItemMeta(id);

            if (masterData == null)
            {
                Debug.LogWarning("masterdata not found id = " + id);
            }
            quantity = masterData.maxStack;
        }
    }
}