using System.Collections.Generic;
using UnityEngine;

namespace TriTailorShop.Data
{
    [CreateAssetMenu(menuName = "TriTailorShop/CraftingTableAsset")]
    public class CraftingTableAsset : ScriptableObject
    {
        public List<CraftingFormula> craftingDefinitions;
    }
}