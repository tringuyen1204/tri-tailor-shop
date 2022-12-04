using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using TriTailorShop.Data;
using UnityEngine;

namespace TriTailorShop.Spine
{
    [RequireComponent(typeof(SkeletonRenderer))]
    public class SpineRegionChangerBehaviour : MonoBehaviour
    {
        [SerializeField] protected EquipmentSpinePropertiesAsset helmetAsset;
        [SerializeField] protected EquipmentSpinePropertiesAsset clothAsset;

        private SkeletonRenderer m_renderer;
        
        private void Awake()
        {
            m_renderer = GetComponent<SkeletonRenderer>();
        }

        public void EquipEquipment(ItemType type, string id)
        {
            EquipmentSpinePropertiesAsset asset = null;

            if (type == ItemType.Cloth)
            {
                asset = clothAsset;
            }
            else if (type == ItemType.Helmet)
            {
                asset = helmetAsset;
            }

            if (asset != null)
            {
                Atlas atlas = asset.atlasAsset.GetAtlas();
                if (atlas == null) return;
                float scale = m_renderer.skeletonDataAsset.scale;

                var data = asset.FindItemMeta(id);
                if (data == null) return;

                foreach (AttachmentDefinition definition in data.spineAttachmentDefinitions)
                {
                    Slot slot = m_renderer.Skeleton.FindSlot(definition.slot);
                    Attachment originalAttachment = slot.Attachment;
                    AtlasRegion region = atlas.FindRegion(definition.region);

                    slot.Attachment = originalAttachment.GetRemappedClone(region, true, true, scale);
                }
            }
        }
    }
}