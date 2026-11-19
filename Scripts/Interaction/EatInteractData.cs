

namespace GettingRidOfIt
{
    public class EatInteractData : InteractData
    {
        public override bool CheckIfCanInteract(InteractItem interactItem) => true;

        public override bool IsAvailable(InteractItem interactItem)
        {
            if (interactItem.GetFood() != null)
            {
                return interactItem.GetFood().SpriteRenderer.sprite != interactItem.GetFood().EmptyPlate;
            }

            return false;
        }


        public override void Interact(InteractItem interactItem)
        {
            base.Interact(interactItem);

            interactItem.GetFood().Eat();
        }
    }
}
