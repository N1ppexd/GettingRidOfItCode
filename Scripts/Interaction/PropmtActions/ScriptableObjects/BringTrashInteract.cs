using UnityEngine;

namespace GettingRidOfIt
{
    public class BringTrashInteract : InteractData
    {
        public override bool CheckIfCanInteract(InteractItem interactItem)
        {
            //if the trash bag has a bodypart inside, it can be taken out
            if(TryGetTrash(Player.Instance.Inventory, out TrashBagInteract trash))
            {
                try
                {
                    if (trash.GetHolder().HolderInventory.HasItems())
                    {
                        foreach (var item in trash.GetHolder().HolderInventory.GetItems())
                            return item.ItemSo.itemType == ItemType.bodypart;
                    }
                }
                catch
                {
                    Debug.Log("MITÄ VITTUA?");
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if it's even shown in the ui in the first place
        /// </summary>
        /// <param name="interactItem"></param>
        /// <returns></returns>
        public override bool IsAvailable(InteractItem interactItem)
        {
            return true;
        }

        /// <summary>
        /// Actually do the interact action
        /// </summary>
        /// <param name="interactItem"></param>
        public override void Interact(InteractItem interactItem)
        {
            base.Interact(interactItem);
            GameEvents.BringTrash();
        }

        /// <summary>
        /// Try to get the trash bag from the inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="trash"></param>
        /// <returns></returns>
        private bool TryGetTrash(InventorySystem inventory,out TrashBagInteract trash)
        {
            trash = null;
            if (inventory == null)
                return false;

            if (inventory == null)
                return false;

            if (!inventory.HasItems())
                return false;

            foreach (var item in inventory.GetItems())
            {
                if (item.GetTrashBag() != null)
                {
                    trash = item.GetTrashBag();
                    return true;
                }
            }

            return false;
        }
    }
}
