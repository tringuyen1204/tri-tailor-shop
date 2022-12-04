using System;
using System.Collections.Generic;
using TriTailorShop.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace TriTailorShop.Spine
{
    [Serializable]
    public class EquipmentSpineProperty
    {
        public string id;
        public List<AttachmentDefinition> spineAttachmentDefinitions;
    }
}