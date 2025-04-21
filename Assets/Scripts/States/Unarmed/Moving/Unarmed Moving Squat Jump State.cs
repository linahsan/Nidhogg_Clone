using States.Unarmed.Still;
using UnityEngine;

namespace States.Unarmed.Moving
{
    public class UnarmedMovingSquatJump : UnarmedMovingState
    {
        public UnarmedMovingSquatJump(PlayerManager manager, Animator animator) : base(manager, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
            animator.SetTrigger("DoUnarmedMovingSquatJump");
        }

        public override void Update()
        {
            base.Update();
            //FALLING LOGIC

            //CHECK GROUNDING
        }
    }
}
