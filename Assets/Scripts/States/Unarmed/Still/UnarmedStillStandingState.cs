using States.Unarmed.Moving;
using UnityEngine;

namespace States.Unarmed.Still
{
    public class UnarmedStillStandingState : UnarmedStillState
    {
        public UnarmedStillStandingState(PlayerManager manager, Animator animator) : base(manager, animator)
        {
            //Debug.Log(animator);
        }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log(animator);
            animator.SetTrigger("DoUnarmedStillStanding");
        }

        protected override void RightPressed()
        {
            base.RightPressed();
            manager.dir = 1;
            manager.ChangeState(new UnarmedMovingAdvancingState(manager, animator));
        }

        protected override void LeftPressed()
        {
            base.LeftPressed();
            manager.dir = -1;
            manager.ChangeState(new UnarmedMovingAdvancingState(manager, animator));
        }

        protected override void DownPressed()
        {
            base.DownPressed();
            manager.ChangeState(new UnarmedStillCrouch(manager, animator));
        }

        protected override void AttackPressed()
        {
            base.LeftPressed();
            manager.ChangeState(new UnarmedStillStandingAttackingState(manager, animator));
        }

        protected override void JumpPressed()
        {
            base.JumpPressed();
            manager.ChangeState(new UnarmedMovingJumpingState(manager, animator));
        }
    }

}