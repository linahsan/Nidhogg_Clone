using States;
using UnityEngine;

public abstract class StillState : PlayerState
{
    // inherit from base class, level 1
    public StillState(PlayerManager manager, Animator animator) : base(manager, animator)
    {
        if (manager.dir == -1)
        {
            
        }
    }
    
    public override void Enter()
    {
        
    }

    public override void Update()
    {
        // standing state behaviour logic
    }

    public override void Exit()
    {
        
    }
}