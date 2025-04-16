namespace States
{
    public abstract class PlayerState
    {
        // base class
        protected PlayerManager manager;

        public PlayerState(PlayerManager manager)
        {
            this.manager = manager;
        }

        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {
        
        }

        public virtual void Update()
        {
        
        }
    
        public virtual PlayerState HandleInput()
        {
            return null;
        }
    }
}
