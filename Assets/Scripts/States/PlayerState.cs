using UnityEngine;

namespace States
{
    public abstract class PlayerState
    {
        // base class
        protected PlayerManager manager;
        protected Animator animator;
        public PlayerState(PlayerManager manager, Animator animator) 
        {
            this.manager = manager;
            this.animator = animator;
            //Need to clean this up later
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
