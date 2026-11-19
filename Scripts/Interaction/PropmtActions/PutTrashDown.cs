
using System.Linq;

namespace GettingRidOfIt
{
    public class PutTrashDown : InteractData
    {
        public override void Interact(InteractItem interactItem)
        {
            base.Interact(interactItem);
            interactItem.GetTrashBag().PutDown();
        }

        public override bool IsAvailable(InteractItem interactItem)
        {
            return interactItem.GetTrashBag() != null && Player.Instance.Inventory.HasItems() && Player.Instance.Inventory.GetItems().Contains(interactItem);
        }
        public override bool CheckIfCanInteract(InteractItem interactItem)
        {
            return interactItem.GetTrashBag() != null && Player.Instance.Inventory.HasItems() && Player.Instance.Inventory.GetItems().Contains(interactItem);
        }
    }
}
