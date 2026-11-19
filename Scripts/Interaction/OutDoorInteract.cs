using UnityEngine;

namespace GettingRidOfIt
{
    public class OutDoorInteract : InteractItem
    {
        public override void DefaultInteract(bool onOrOff)
        {

        }

        public override OutDoorInteract GetOutDoor() => this;
    }
}
