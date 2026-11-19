
namespace GettingRidOfIt
{
    public class PickTrashBag : InteractData
    {
        public override bool CheckIfCanInteract(InteractItem interactItem) => interactItem.GetTrashBag() != null;

        public override void Interact(InteractItem interactItem)
        {
            base.Interact(interactItem);
            interactItem.GetTrashBag().Pickup();
        }

        public override bool IsAvailable(InteractItem interactItem) => interactItem.GetTrashBag() != null;
    }
}
