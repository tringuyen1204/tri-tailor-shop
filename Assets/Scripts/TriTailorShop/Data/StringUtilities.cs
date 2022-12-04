using UnityEngine;

namespace TriTailorShop.Data
{
    public static class StringUtilities
    {
        private const string HEX_SOURCE = "0123456789ABCDEF";

        public static string GenerateHexId(int length)
        {
            string ret = "";
            
            for (int a = 0; a < length; a++)
            {
                var random = Random.Range(0, 16);
                ret += HEX_SOURCE[random];
            }

            return ret;
        }
    }
}