using UnityEngine;

public class StillState : PlayerState
{
    // inherit from base class, level 1
    public StillState(PlayerManager manager) : base(manager)
    {
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
