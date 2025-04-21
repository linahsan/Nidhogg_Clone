using States.Unarmed.Still;
using UnityEngine;

namespace States.Unarmed.Moving
{
    public class UnarmedMovingDivekick : UnarmedMovingState
    {
        public UnarmedMovingDivekick(PlayerManager manager, Animator animator) : base(manager, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
            animator.SetTrigger("DoUnarmedMovingDivekick");
        }

        
    }
}
