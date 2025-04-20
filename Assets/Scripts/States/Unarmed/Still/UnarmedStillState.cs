using UnityEngine;

namespace States.Unarmed.Still
{
    public abstract class UnarmedStillState : UnarmedState
    {
        public UnarmedStillState(PlayerManager manager, Animator animator) : base(manager, animator)
        {
            
        }

    }

}