using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

namespace TriTailorShop.Spine
{
    [CreateAssetMenu(menuName = "TriTailorShop/EquipmentDefinitionAsset")]
    public class EquipmentSpinePropertiesAsset : ScriptableObject
    {
        public SpineAtlasAsset atlasAsset;
        public float scale;
        
        [FormerlySerializedAs("equipmentMetas")] public List<EquipmentSpineProperty> spineProperties;
        
        public EquipmentSpineProperty FindItemMeta(string id)
        {
            foreach (var data in spineProperties)
            {
                if (data.id == id)
                {
                    return data;
                }
            }
            return null;
        }
    }
}