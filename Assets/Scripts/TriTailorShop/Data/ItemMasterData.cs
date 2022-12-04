using System;
using UnityEngine;

namespace TriTailorShop.Data
{
    [Serializable]
    public class ItemMasterData
    {
        public string id;
        public ItemType type;

        public Sprite icon;
        
        // ignore if it's not material
        public int maxStack;
        public int shopPrice;
    }
}