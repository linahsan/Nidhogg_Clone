using States.Unarmed.Still;
using UnityEngine;

namespace States.Unarmed.Moving
{
    public class UnarmedMovingRunningState : UnarmedMovingState
    {
        public UnarmedMovingRunningState(PlayerManager manager, Animator animator) : base(manager, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
            animator.SetTrigger("DoUnarmedMovingAdvancing");
        }

        public override void Update()
        {
            //ACTUAL RUNNING

            base.Update();
            if(manager.dir == 1 && !manager.playerInput.actions["Right"].IsPressed())
            {
                manager.ChangeState(new UnarmedStillStandingState(manager, animator));
            }

            if(manager.dir == -1 && !manager.playerInput.actions["Left"].IsPressed())
            {
                manager.ChangeState(new UnarmedStillStandingState(manager, animator));
            }

        }
    }
}
