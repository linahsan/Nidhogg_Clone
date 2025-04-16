namespace States.Armed.Still
{
    public class ArmedStillStandingState : ArmedStillState
    {
        private enum SwordPosition
        {
            Lower = 0, Middle = 1, Upper = 2, Raising = 3
        }
        private SwordPosition _swordPosition;
        
        public ArmedStillStandingState(PlayerManager manager) : base(manager)
        {
            
        }

        protected override void GotoAttackingState()
        {
            manager.ChangeState(new ArmedStillStandingAttackingState(manager));
        }

        protected override void GotoDisarmedState()
        {
            throw new System.NotImplementedException();
        }

        public override PlayerState HandleInput()
        {
            base.HandleInput();
            // If there's key affect sword position, handle it
            
            // If now player is raising the sword yet the raising key is not pressed, then back to upper
            if (_swordPosition == SwordPosition.Raising)
                _swordPosition = SwordPosition.Upper;
            
            // If attack pressed
            GotoAttackingState();
            
            return this;
        }

        private void MoveSwordUp()
        {
            if (_swordPosition == SwordPosition.Raising)
                return;
            _swordPosition++;
        }

        private void MoveSwordDown()
        {
            if (_swordPosition == SwordPosition.Lower)
                return;
            _swordPosition--;
        }
    }
}