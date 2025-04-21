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
            animator.SetTrigger("DoUnarmedMovingRunning");
        }

        public override void Update()
        {
            //ACTUAL RUNNING
            manager.gameObject.GetComponent<Transform>().position += new Vector3(manager.runSpeed/50 * manager.dir, 0, 0);

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

        //IMPLEMENT OTHER THINGS YOU CAN DO WHILE RUNNING
    }
}
