using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GettingRidOfIt
{
    public abstract class PlayerBaseState
    {
        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract void CheckSwitchStates();
    }
}
