using UnityEngine;

namespace GettingRidOfIt
{
    public class PutDownItem : InteractData
    {

        public override bool IsAvailable(InteractItem interactItem)
        {
            Debug.Log($"interactItem picked up : {interactItem.interacted}");
            return interactItem.interacted;
        }

        public override bool CheckIfCanInteract(InteractItem interactItem)
        {
            return true;
        }

        public override void Interact(InteractItem interactItem)
        {
            base.Interact(interactItem);
            interactItem.DefaultInteract(false);
        }
    }
}
