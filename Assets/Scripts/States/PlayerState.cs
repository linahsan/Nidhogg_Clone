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
            animator = manager.gameObject.GetComponent<Animator>();
            //Debug.Log(animator);
            
            //Need to clean this up later
        }
        

        public virtual void Enter()
        {
            this.manager = manager;
            animator = manager.gameObject.GetComponent<Animator>();
            //Debug.Log(animator);
        }

        public virtual void Exit()
        {
            
        }

        public virtual void Update()
        {
            HandleInput();
            manager.gameObject.GetComponent<Transform>().localScale = new Vector3(manager.dir, 1, 1);
        }
    
        public virtual PlayerState HandleInput()
        {
            //Debug.Log("happened 2");
            if(manager.playerInput.actions["Right"].IsPressed())
            {
                RightPressed();
                //Debug.Log("happened");
            }

            if(manager.playerInput.actions["Left"].IsPressed())
            {
                LeftPressed();
            }

            if(manager.playerInput.actions["Up"].IsPressed())
            {
                UpPressed();
            }

            if(manager.playerInput.actions["Down"].IsPressed())
            {
                DownPressed();
            }

            if(manager.playerInput.actions["Jump"].IsPressed())
            {
                JumpPressed();
            }

            if(manager.playerInput.actions["Attack"].IsPressed())
            {
                AttackPressed();
            }
            
            return null;
        }

        protected virtual void RightPressed()
        {

        }
        protected virtual void LeftPressed()
        {
            
        }
        protected virtual void UpPressed()
        {
            
        }
        protected virtual void DownPressed()
        {
            
        }
        protected virtual void JumpPressed()
        {
            
        }
        protected virtual void AttackPressed()
        {
            
        }
    }
}
