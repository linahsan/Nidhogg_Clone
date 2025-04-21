using UnityEngine;

namespace States.Armed.Still
{
    public class ArmedStillJumpingState : ArmedStillState
    {
        public ArmedStillJumpingState(PlayerManager manager, Animator animator) : base(manager, animator)
        {
        }

        protected override void GotoAttackingState()
        {
            throw new System.NotImplementedException();
        }

        protected override void GotoDisarmedState()
        {
            throw new System.NotImplementedException();
        }
    }
}