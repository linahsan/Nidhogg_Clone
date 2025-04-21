using States.Unarmed.Still;
using UnityEngine;


namespace States.Unarmed.Moving
{
    public class UnarmedMovingFalling : UnarmedMovingState
    {
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

            //CHECK IF GROUNDED
        }

        protected override void AttackPressed()
        {
            base.AttackPressed();
            manager.ChangeState(new UnarmedMovingDivekick(manager, animator));
        }
    }
}
