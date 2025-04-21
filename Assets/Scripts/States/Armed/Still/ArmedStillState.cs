using UnityEngine;

namespace States.Armed.Still
{
    public abstract class ArmedStillState : ArmedState
    {
        public ArmedStillState(PlayerManager manager, Animator animator) : base(manager, animator)
        {
            
        }

    }
}