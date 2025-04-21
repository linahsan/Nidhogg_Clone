using UnityEngine;

namespace States.Unarmed.Still
{
    public class UnarmedStillStandingAttackingState : UnarmedStillState
    {
        public UnarmedStillStandingAttackingState(PlayerManager manager, Animator animator) : base(manager, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
            animator.Play("Unarmed Still Standing Attacking", 0, 0);
        }

        public override void Update()
        {
            base.Update();
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                manager.ChangeState(new UnarmedStillStandingState(manager, animator));
            }
        }

        //We need to check if you can exit out of the attacking animation early in game

    }
}