using States.Unarmed.Still;
using UnityEngine;


namespace States.Unarmed.Moving
{
    public class UnarmedMovingAdvancingState : UnarmedMovingState
    {
        private bool hasReleased = false;
        public UnarmedMovingAdvancingState(PlayerManager manager, Animator animator) : base(manager, animator)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //animator.SetTrigger("DoUnarmedMovingAdvancing");
            animator.Play("Unarmed Moving Advancing", 0, 0);
        }

        public override void Update()
        {
            base.Update();
            //IMPLEMENT ACTUAL CODE

            if(manager.dir == 1 && !manager.playerInput.actions["Right"].IsPressed())
            {
                hasReleased = true;
            }

            if(manager.dir == -1 && !manager.playerInput.actions["Left"].IsPressed())
            {
                hasReleased = true;
            }

            //transitions away:
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                if(hasReleased == true)
                {
                    manager.ChangeState(new UnarmedStillStandingState(manager, animator));
                }else
                {
                    manager.ChangeState(new UnarmedMovingRunningState(manager,animator));
                }
            }
        }
    }

}
