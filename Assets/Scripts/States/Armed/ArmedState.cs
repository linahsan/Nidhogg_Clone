namespace States.Armed
{
    public abstract class ArmedState : PlayerState
    {
        public ArmedState(PlayerManager manager) : base(manager)
        {
            
        }

        public virtual void Disarm()
        {
            manager.hasSword = false;
            GotoDisarmedState();
        }
        
        
        protected abstract void GotoAttackingState();
        protected abstract void GotoDisarmedState();
    }
}