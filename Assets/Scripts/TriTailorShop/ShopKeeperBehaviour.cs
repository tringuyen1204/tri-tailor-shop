using TriTailorShop.GameCharacter;
using UnityEngine;

namespace TriTailorShop
{
    public class ShopKeeperBehaviour : MonoBehaviour
    {
        public PlayerDataAsset asset;

        private NpcShopkeeper m_shopkeeper;
    
        // Start is called before the first frame update
        void Awake()
        {
            m_shopkeeper = new NpcShopkeeper();
            m_shopkeeper.LoadFromAsset(asset);

            NpcShopkeeper.Instance = m_shopkeeper;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
