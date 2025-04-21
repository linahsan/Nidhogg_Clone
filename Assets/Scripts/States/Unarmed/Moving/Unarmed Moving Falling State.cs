using States.Unarmed.Still;
using Unity.VisualScripting;
using UnityEngine;


namespace States.Unarmed.Moving
{

    public class UnarmedMovingFalling : UnarmedMovingState
    {
        //TEMPORARY, FOR TESTING
        private int timer;
        public UnarmedMovingFalling(PlayerManager manager, Animator animator) : base(manager, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
            animator.SetTrigger("Do Unarmed Moving Falling");
        }

        public override void Update()
        {
            base.Update();

            //FALLING
            manager.gameObject.GetComponent<Transform>().position -= new Vector3(0, manager.fallSpeed/50, 0);

            //TEMPORARY, FOR TESTING:
            timer++;
            if(timer >= manager.jumpTime)
            {
                manager.ChangeState(new UnarmedStillStandingState(manager, animator));
            }

            //CHECK IF GROUNDED
        }

        protected override void RightPressed()
        {
            base.RightPressed();
            manager.dir = 1;
            manager.gameObject.GetComponent<Transform>().position += new Vector3(manager.airMoveSpeed/50 * manager.dir, 0, 0);
        }

        protected override void LeftPressed()
        {
            base.LeftPressed();
            manager.dir = -1;
            manager.gameObject.GetComponent<Transform>().position += new Vector3(manager.airMoveSpeed/50 * manager.dir, 0, 0);
        }

        protected override void AttackPressed()
        {
            base.AttackPressed();
            manager.ChangeState(new UnarmedMovingDivekick(manager, animator));
        }


    }
}
