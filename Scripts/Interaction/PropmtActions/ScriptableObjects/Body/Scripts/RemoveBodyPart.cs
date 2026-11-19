using N1ppeUtils;
using System.Collections.Generic;
using UnityEngine;

namespace GettingRidOfIt
{
    public class RemoveBodyPart : InteractData
    {

        public override bool IsAvailable(InteractItem interactItem)
        {
            try
            {
                //Checks if any bodypart is attached to the body
                foreach (var bodyPart in interactItem.GetBody().GetBodyParts())
                {
                    if (bodyPart.isAttached)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }
        public override bool CheckIfCanInteract(InteractItem interactItem)
        {
            return base.CheckAvailability(Player.Instance.Inventory.GetItems(), ItemType.cutter);
        }
        public override void Interact(InteractItem interactItem)
        {
            //Checks if the player has a cutter in their inventory
            if (base.CheckAvailability(Player.Instance.Inventory.GetItems(), ItemType.cutter))
            {
                //randomly detaches a bodypart from the body
                List<BodyParts> parts = new(interactItem.GetBody().GetBodyParts());

                NiilosExtensions.Shuffle(parts);

                foreach (var bodyPart in parts)
                {
                    if (bodyPart.isAttached)
                    {
                        //Removes the bodypart from the body
                        base.Interact(interactItem);
                        interactItem.GetBody().RemoveBodyPart(bodyPart);
                        break;
                    }
                }
            }
        }
    }
}
