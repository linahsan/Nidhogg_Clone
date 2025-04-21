using System.Numerics;
using States.Unarmed.Still;
using UnityEngine;


namespace States.Unarmed.Moving
{
    public class UnarmedMovingJumpingState : UnarmedMovingState
    {
        private int timer;
        private int jumpTime = 60;
        //I think the actual way to implement this is gonne just be giving it a starting momentum, then 
        //subtracting that due to gravity as you go. We'll need to see what arc the game makes though. Iirc
        //the game also has you go higher if you have momentum from running, so we should probably include
        //a momentum variable in manager,then add some function of that to the upward momentum in 
        //this script's enter function
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
                manager.gameObject.GetComponent<Transform>().position += new UnityEngine.Vector3(0, manager.jumpSpeed/50, 0);
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
            manager.dir = 1;
            manager.gameObject.GetComponent<Transform>().position += new UnityEngine.Vector3(manager.airMoveSpeed/50 * manager.dir, 0, 0);
        }

        protected override void LeftPressed()
        {
            base.LeftPressed();
            //MOVE LEFT
            manager.dir = -1;
            manager.gameObject.GetComponent<Transform>().position += new UnityEngine.Vector3(manager.airMoveSpeed/50 * manager.dir, 0, 0);
        }


    }
}
