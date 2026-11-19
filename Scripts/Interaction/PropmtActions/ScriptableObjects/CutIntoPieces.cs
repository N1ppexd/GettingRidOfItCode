namespace GettingRidOfIt
{
    public class CutIntoPieces : InteractData
    {
        public override bool CheckIfCanInteract(InteractItem interactItem)
        {
            return base.CheckAvailability(Player.Instance.Inventory.GetItems(), ItemType.cutter);
        }

        public override bool IsAvailable(InteractItem interactItem)
        {
            return interactItem.GetBodyPart().canDismember;
        }

        public override void Interact(InteractItem interactItem)
        {
            base.Interact(interactItem);
            interactItem.GetBodyPart().Dismember();
        }
    }
}
