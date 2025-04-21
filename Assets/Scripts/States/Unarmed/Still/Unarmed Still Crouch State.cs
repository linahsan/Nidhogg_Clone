using System.Globalization;
using States.Unarmed.Moving;
using UnityEngine;

namespace States.Unarmed.Still
{
    public class UnarmedStillCrouch : UnarmedStillState
    {
        public UnarmedStillCrouch(PlayerManager manager, Animator animator) : base(manager, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
            animator.SetTrigger("DoUnarmedStillCrouch");
        }

       

        protected override void RightPressed()
        {
            base.RightPressed();
            manager.dir = 1;

        }

        protected override void LeftPressed()
        {
            base.LeftPressed();
            manager.dir = -1;
        }

        protected override void UpPressed()
        {
            base.UpPressed();
            manager.ChangeState(new UnarmedStillStandingState(manager, animator));
        }

        protected override void AttackPressed()
        {
            base.AttackPressed();
            manager.ChangeState(new UnarmedStillSweepkick(manager, animator));
        }

        protected override void JumpPressed()
        {
            base.JumpPressed();
            manager.ChangeState(new UnarmedMovingSquatJump(manager, animator));
        }
    }
}