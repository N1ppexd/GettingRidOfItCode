using UnityEngine;

namespace GettingRidOfIt
{
    public class CookInteractData : InteractData
    {
        public override bool CheckIfCanInteract(InteractItem interactItem)
        {
            if (Player.Instance.Inventory.HasItems())
            {
                foreach (var item in Player.Instance.Inventory.items)
                {
                    if (item.ItemSo == null)
                    {
                        continue;
                    }

                    if (item.ItemSo.itemType == ItemType.bodypart)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void Interact(InteractItem interactItem)
        {
            base.Interact(interactItem);
            // Do something
            Debug.Log("cooking the bodypart....");
            interactItem.DefaultInteract(true);
        }


        /// <summary>
        /// Does the player carry a body part around? If yes, then the cooking action can be done.
        /// </summary>
        /// <param name="interactItem"></param>
        /// <returns></returns>
        public override bool IsAvailable(InteractItem interactItem)
        {
            return true;
        }
    }
}
