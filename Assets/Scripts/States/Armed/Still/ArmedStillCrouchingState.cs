namespace States.Armed.Still
{
    public class ArmedStillCrouchingState : ArmedStillState
    {
        public ArmedStillCrouchingState(PlayerManager manager) : base(manager)
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