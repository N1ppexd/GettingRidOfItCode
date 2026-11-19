using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace GettingRidOfIt
{
    public class DefaultInteract : InteractData
    {


        public bool requireItem;

        [Space(30)]
        [LabelText("item type")]
        [ShowIf("requireItem")]
        [DetailedInfoBox("Item type required", "What is the type of the item needed to interact with this object?")]
        [HideLabel]
        public ItemType itemType;


        [Space(5)]
        [LabelText("Interact positive or negative")]
        [DetailedInfoBox("Details:", "Does this interact make the object do something, or undo something? " +
            "If it undoes it, se this to false, otherwise set this to true.")]
        [HideLabel]
        public bool interactOnOrOff = true;

        /// <summary>
        /// Check if the whole action is even available in the first place.
        /// </summary>
        /// <param name="interactItem"></param>
        /// <returns></returns>
        public override bool IsAvailable(InteractItem interactItem)
        {
            if(interactItem == null || !interactItem.isActiveAndEnabled) return false;

            return !(interactOnOrOff == interactItem.interacted);
        }

        /// <summary>
        /// Check if the player has the right tools to perform this action.
        /// </summary>
        /// <param name="interactItem"></param>
        /// <returns></returns>
        public override bool CheckIfCanInteract(InteractItem interactItem)
        {
            //if the action does not require an item, the action can always be performed
            if(requireItem == false) return true;

            return base.CheckAvailability(Player.Instance.Inventory.GetItems(), ItemType.cleaner);
        }


        public override void Interact(InteractItem interactItem)
        {
            base.Interact(interactItem);
            interactItem.GetItem().DefaultInteract(interactOnOrOff);
        }
    }
}
