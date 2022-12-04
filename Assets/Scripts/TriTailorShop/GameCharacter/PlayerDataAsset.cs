using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TriTailorShop.GameCharacter
{
    [CreateAssetMenu(menuName = "TriTailorShop/PlayerDataAsset")]
    public class PlayerDataAsset : ScriptableObject
    {
        public int gold;
        public int fur;
        public int leather;
        public int cotton;
        public int silk;
        
        public int maxSlot;
        [FormerlySerializedAs("inventory")] public List<string> itemIds;
    }
}