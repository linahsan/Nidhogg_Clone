using States.Unarmed.Still;
using UnityEngine;


namespace States.Unarmed.Moving
{
    public class UnarmedMovingJumpingState : UnarmedMovingState
    {
        private int timer;
        private int jumpTime = 60;
        public UnarmedMovingJumpingState(PlayerManager manager, Animator animator) : base(manager, animator)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            animator.SetTrigger("DoUnarmedMovingJumping");
        }

        public override void Update()
        {
            base.Update();
            timer++;
            if(timer < jumpTime)
            {
                //UPWARD JUMP LOGIC
            }
            else
            {
                manager.ChangeState(new UnarmedMovingFalling(manager, animator));
            }
        }

        protected override void AttackPressed()
        {
            base.AttackPressed();
            manager.ChangeState(new UnarmedMovingDivekick(manager, animator));
        }

        protected override void RightPressed()
        {
            base.RightPressed();
            //MOVE RIGHT
        }

        protected override void LeftPressed()
        {
            base.LeftPressed();
            //MOVE LEFT
        }


    }
}
