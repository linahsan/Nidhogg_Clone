using States;
using States.Armed.Still;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Animator animator;
    public PlayerInput playerInput;
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


        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        //ResetState();Somebody says pointless, blame him . :(
        state = ChangeState(new ArmedStillStandingState(this, animator));
        
   
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

    public PlayerState ChangeState(PlayerState newState)
    {
        if (newState == null)
        {
            Debug.LogError("newState is null");
            return null;
        }
        

        state?.Exit();
        state = newState;
        state.Enter();
        return newState;
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
