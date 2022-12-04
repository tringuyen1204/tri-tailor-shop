using System.Collections.Generic;
using UnityEngine;

namespace TriTailorShop.Data
{
    public static class ItemUtilities
    {
        private static Dictionary<string, ItemMasterData> _masterDatas;

        public static void Reset()
        {
            _masterDatas = null;
        }

        public static Sprite GetIconFromId(string itemId)
        {
            var masterData = GetItemMeta(itemId);
            return masterData?.icon;
        }
        
        public static ItemMasterData GetItemMeta(string id)
        {
            if (_masterDatas == null)
            {
                var asset = Resources.Load<ItemTableAsset>("item_master_data_table");
                _masterDatas = new Dictionary<string, ItemMasterData>();

                foreach (var data in asset.datas)
                {
                    _masterDatas[data.id] = data;
                }
            }

            if (_masterDatas.ContainsKey(id))
            {
                return _masterDatas[id];
            }

            return null;
        }
    }
}