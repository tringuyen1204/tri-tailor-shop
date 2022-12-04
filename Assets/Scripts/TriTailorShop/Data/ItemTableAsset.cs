using System.Collections.Generic;
using UnityEngine;

namespace TriTailorShop.Data
{
    [CreateAssetMenu(menuName = "TriTailorShop/ItemTableAsset")]
    public class ItemTableAsset : ScriptableObject
    {
        public List<ItemMasterData> datas;
    }
}