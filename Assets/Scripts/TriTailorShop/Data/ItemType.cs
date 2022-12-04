using System;

namespace TriTailorShop.Data
{
    public enum ItemType {
        Cloth  = 1,
        Helmet = 1 << 1,
        Material = 1 << 2,
    }
}