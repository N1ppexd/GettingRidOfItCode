using System.Collections.Generic;
using UnityEngine;

namespace GettingRidOfIt
{
    public class PutItemIn : InteractData
    {

        public List<ItemType> acceptedItemTypes = new();

        public override bool CheckIfCanInteract(InteractItem interactItem)
        {
            return true;
        }

        public override void Interact(InteractItem interactItem)
        {
            base.Interact(interactItem);
            Debug.Log("put the item into the thing");
            InteractItem[] inventoryItems = Player.Instance.Inventory.GetItems();
            //hyvin hyvin paskaa koodia
            interactItem.GetHolder().PlaceItem(inventoryItems[inventoryItems.Length - 1]);
        }

        public override bool IsAvailable(InteractItem interactItem)
        {
            //if it either is opened and the player has the item or it is not opened and the player has the item, but the holder accepts items even when not opened
            if(Player.Instance.Inventory.items.Count > 0 && (interactItem.interacted || !interactItem.GetHolder().HasToOpen))
            {
                if(interactItem.GetHolder().HolderInventory.HasItems())
                {
                    return false;
                }

                foreach(InteractItem item in Player.Instance.Inventory.items)
                {
                    if(acceptedItemTypes.Contains(item.ItemSo.itemType))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
