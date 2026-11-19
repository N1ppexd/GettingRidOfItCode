using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GettingRidOfIt
{
    public class PlayerStateFactory
    {
        Player context;

        public PlayerStateFactory(Player context)
        {
            this.context = context;
        }



        public PlayerBaseState DefaultState()
        {
            return new PlayerDefaultState();
        }

        public PlayerBaseState PlayerDragState()
        {
            return new PlayerDragState();
        }

        public PlayerBaseState PlayerTeleportState()
        {
            return new PlayerTeleportState();
        }
    }
}
