using UnityEngine.Events;
using UnityEngine.Rendering;

namespace TriTailorShop.Data
{
    public class ResourceData
    {
        private readonly string m_id;
        private int m_quantity;
        
        public string Id => m_id;
        public int Quantity => m_quantity;
        
        // invoke event with old and new value after change
        public readonly UnityEvent<int, int> onValueChanged = new UnityEvent<int, int>();

        public ResourceData(string resourceId)
        {
            m_id = resourceId;
        }
        
        /// <summary>
        /// Try to spend resource and return true if successful
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>Must be positive</returns>
        public bool TrySpend(int amount)
        {
            // for resource, I always avoid potential mistakes from users
            // no negative mean we can distinguish between buy and sell
            if (amount < 0) return false;
            
            if (m_quantity >= amount)
            {
                int oldValue = m_quantity;
                m_quantity -= amount;
                
                onValueChanged.Invoke(oldValue, m_quantity);
                return true;
            }

            return false;
        }

        public void Add(int amount)
        {
            int oldValue = m_quantity;
            m_quantity += amount;
            onValueChanged.Invoke(oldValue, m_quantity);
        }
    }
}