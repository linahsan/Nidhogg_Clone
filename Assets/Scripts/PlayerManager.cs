using States;
using States.Armed.Still;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Animator animator;
    public int dir = -1;
    public bool hasSword;
    public bool isGrounded;
    public bool isDowned;
    public bool isDead;
    
    //will be discuss later
    public PlayerState state;
    
    void Start()
    {
        hasSword = true;
        isGrounded = true; // change when refining game feel (players jump in)
        isDowned = false;
        isDead = false;
        //ResetState();Somebody says pointless, blame him . :(
        ChangeState(new ArmedStillStandingState(this));
        
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (isDead)
        {
            
        }

        if (isDowned)
        {
            
        }

        if (hasSword)
        {
            
        }
        else
        {
            
        }
        //state update
        state?.Update();//checks if state exist
    }

    public void ChangeState(PlayerState newState)
    {
        if (newState == null)
        {
            Debug.LogError("newState is null");
            return;
        }
        

        state?.Exit();
        state = newState;
        state.Enter();
    }

    void ResetState()
    {
        if (hasSword)
        {
            
        }
        else
        {
            
        }
    }
}
