using UnityEngine;

namespace States.Unarmed
{
    public abstract class UnarmedState : PlayerState
    {
        public UnarmedState(PlayerManager manager, Animator animator) : base(manager, animator)
        {
            
        }
    }
}