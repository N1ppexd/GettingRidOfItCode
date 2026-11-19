

namespace GettingRidOfIt
{
    public class PickUpItem : InteractData
    {
        public override bool IsAvailable(InteractItem interactItem)
        {
            return !interactItem.interacted;
        }
        public override bool CheckIfCanInteract(InteractItem interactItem)
        {
            return true;
        }

        public override void Interact(InteractItem interactItem)
        {
            base.Interact(interactItem);

            if(interactItem.interacted)
            {
                interactItem.DefaultInteract(false);
                interactItem.interacted = false;
            }
            else
            {
                interactItem.DefaultInteract(true);
                interactItem.interacted = true;
            }
            
        }
    }
}
