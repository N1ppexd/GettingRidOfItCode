using UnityEngine;

namespace GettingRidOfIt
{
    public class DetachRemaining : InteractData
    {
        public override bool CheckIfCanInteract(InteractItem interactItem)
        {
            if (Player.Instance.Inventory.HasItems())
            {
                foreach (var item in Player.Instance.Inventory.GetItems())
                {
                    if (item.ItemSo.itemType == ItemType.cutter)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override void Interact(InteractItem interactItem)
        {
            if (interactItem.GetBody() == null)
            {
                Debug.Log("body is null");
                return;
            }
            base.Interact(interactItem);
            //detach all the remaining parts (IN THIS CASE CUT THE ENTIRE BODY IN HALF)
            interactItem.GetBody().DetachRemainingParts();
        }

        public override bool IsAvailable(InteractItem interactItem)
        {
            if(interactItem.GetBody() == null)
            {
                Debug.Log("body is null");
                return false;
            }


            return interactItem.GetBody().CanDetachRemainingParts();
        }
    }
}
