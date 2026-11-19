using UnityEngine;
using System.Collections.Generic;

namespace GettingRidOfIt
{
    public class InventorySystem
    {
        //how many slots the inventory has
        private int slots;

        public List<InteractItem> items { get; private set; } = new List<InteractItem>();

        public InventorySystem(int slots)
        {
            this.slots = slots;
        }

        /// <summary>
        /// returns true if the inventory contains any items
        /// </summary>
        /// <returns></returns>
        public bool HasItems() { return items.Count > 0; }

        public InteractItem[] GetItems()
        {
            try { return items.ToArray(); }
            catch { return null; }
        }


        public int GetSlots() { return slots; }

        public bool CanAddItem()
        {
            return items.Count + 1 <= slots;
        }

        /// <summary>
        /// Add <paramref name="item"/> to inventory
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(InteractItem item)
        {
            if(items.Count == slots)
            {

                //hyvin paskaa koodia mutta mitä väliä nytten kun se toimii
                var lastItem = items[slots - 1];

                if (lastItem.GetTrashBag() != null)
                {
                    lastItem.GetTrashBag().PutDown();
                }
                else
                {
                    lastItem.DefaultInteract(false);
                }
            }

            items.Add(item);
        }

        /// <summary>
        /// Remove item, if the inventory contains it.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(InteractItem item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                item.interacted = false;

                if(item.GetTrashBag() != null) item.GetTrashBag().PickedUp = false;
            }
        }
    }
}
