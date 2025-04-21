using UnityEngine;

namespace States.Armed
{
    public abstract class ArmedState : PlayerState
    {
        public ArmedState(PlayerManager manager, Animator animator) : base(manager, animator)
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