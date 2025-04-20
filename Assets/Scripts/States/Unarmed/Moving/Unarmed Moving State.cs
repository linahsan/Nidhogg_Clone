using UnityEngine;

namespace States.Unarmed.Still
{
    public abstract class UnarmedMovingState : UnarmedState
    {
        public UnarmedMovingState(PlayerManager manager, Animator animator) : base(manager, animator)
        {
            
        }

    }

}