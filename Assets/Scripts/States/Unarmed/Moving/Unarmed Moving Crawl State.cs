using States.Unarmed.Still;
using UnityEngine;

namespace States.Unarmed.Moving
{
    public class UnarmedMovingCrawl : UnarmedMovingState
    {
        public UnarmedMovingCrawl(PlayerManager manager, Animator animator) : base(manager, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
            animator.SetTrigger("DoUnarmedMovingCrawl");
        }

        public override void Update()
        {
            base.Update();
            if(manager.dir == 1 && !manager.playerInput.actions["Right"].IsPressed())
            {
                manager.ChangeState(new UnarmedStillCrouch(manager, animator));
            }

            if(manager.dir == -1 && !manager.playerInput.actions["Left"].IsPressed())
            {
                manager.ChangeState(new UnarmedStillCrouch(manager, animator));
            }
        }


    }
}