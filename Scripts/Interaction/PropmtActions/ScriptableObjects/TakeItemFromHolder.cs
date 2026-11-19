using UnityEngine;

namespace GettingRidOfIt
{
    public class TakeItemFromHolder : InteractData
    {

        /// <summary>
        /// When it's deemed that the player is capable of interacting, this method is called
        /// </summary>
        /// <param name="item"></param>
        public override void Interact(InteractItem item)
        {
            Debug.Log("Take item from holder");
            Holder holder = item.GetHolder();
            if(holder != null)
            {
                var itemToTake = holder.TakeItem();
                if(itemToTake != null)
                {
                    base.Interact(item);
                    itemToTake.DefaultInteract(true);
                    //Player.Instance.Inventory.AddItem(itemToTake);
                }
            }
        }

        /// <summary>
        /// This check is done before the interact action is shown in the UI
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool IsAvailable(InteractItem item)
        {
            Holder holder = item.GetHolder();
            return holder != null && holder.HolderInventory.HasItems() && item.interacted;
        }


        /// <summary>
        /// After the interact action is shown in the UI, check if the player can interact with the item
        /// </summary>
        /// <param name="interactItem"></param>
        /// <returns></returns>
        public override bool CheckIfCanInteract(InteractItem interactItem)
        {
            Holder holder = interactItem.GetHolder();
            return holder != null && holder.HolderInventory.HasItems() && interactItem.interacted;
        }

    }
}
