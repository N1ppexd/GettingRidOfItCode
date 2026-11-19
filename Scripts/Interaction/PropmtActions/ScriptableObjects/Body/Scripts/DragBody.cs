using UnityEngine;

namespace GettingRidOfIt
{
    public class DragBody : InteractData
    {
        public override bool IsAvailable(InteractItem interactItem)
        {
            return true;
        }
        public override bool CheckIfCanInteract(InteractItem interactItem)
        {
            return true;
        }
        public override void Interact(InteractItem interactItem)
        {
            Debug.Log("interact");
        }
    }
}
