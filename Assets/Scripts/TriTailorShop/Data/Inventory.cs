using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TriTailorShop.Data
{
    public class Inventory
    {
        protected int maxSlot;
        protected readonly List<ItemData> items = new List<ItemData>();

        public int MaxSlot => maxSlot;
        public bool IsFull => maxSlot <= items.Count;
        
        public readonly UnityEvent onInventoryChanged = new UnityEvent();

        public void SetMaxSlot(int value)
        {
            maxSlot = value;
        }

        public bool AddItem(ItemData item)
        {
            // can not add item if the inventory is full
            if (items.Count < maxSlot)
            {
                items.Add(item);
                onInventoryChanged.Invoke();
                return true;
            }
            return false;
        }

        public bool InsertItem(ItemData item, int index)
        {
            if (!IsFull)
            {
                items.Insert(index, item);
                onInventoryChanged.Invoke();
                return true;
            }

            Debug.LogError("inventory is full, cannot insert item");
            return false;
        }
        
        public ItemData GetItemAt(int index)
        {
            if (index >= items.Count || index > maxSlot)
            {
                return null;
            }
            return items[index];
        }
        
        public bool RemoveItem(int index)
        {
            if (index <= items.Count && index <= maxSlot)
            {
                items.RemoveAt(index);
                onInventoryChanged.Invoke();
                return true;
            }
            return false;
        }

        public bool SwapItem(ItemData itemIn, ItemData itemOut)
        {
            var index = FindItemIndex(itemOut.instanceId);
            if (index < 0)
            {
                Debug.LogError("Cannot found item in inventory, id = " + itemOut.instanceId);
                return false;
            }

            RemoveItem(index);

            if (itemIn != null)
            {
                InsertItem(itemIn, index);
            }
            
            return true;
        }

        public int FindItemIndex(string instanceId)
        {
            for (var a = 0; a < items.Count; a++)
            {
                if (items[a].instanceId == instanceId)
                {
                    return a;
                }
            }

            return -1;
        }
    }
}