using System.Net.Mail;
using UnityEngine;

namespace States.Unarmed.Still
{
    public class UnarmedStillSweepkick : UnarmedStillState
    {
        public UnarmedStillSweepkick(PlayerManager manager, Animator animator) : base(manager, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
            animator.Play("Unarmed Still Sweepkick", 0, 0);
        }

        public override void Update()
        {
            base.Update();
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                manager.ChangeState(new UnarmedStillCrouch(manager, animator));
            }
        }
    }
}
