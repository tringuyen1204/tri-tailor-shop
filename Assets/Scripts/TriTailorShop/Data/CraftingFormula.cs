using System;

namespace TriTailorShop.Data
{
    [Serializable]
    public class CraftingFormula
    {
        public string equipId;
        public ResourceRequirement[] requirements;
    }
}