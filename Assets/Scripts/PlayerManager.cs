using States;
using States.Armed.Still;
using States.Unarmed.Still;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Animator animator;
    public PlayerInput playerInput;

    public GameObject punchColliderObject;
    public BoxCollider2D punchCollider; 
    public ColliderTag punchColliderTag;
    public int dir = -1;
    public bool hasSword;
    public bool isGrounded;
    public bool isDowned;
    public bool isDead;

    //Movement tuning:
    public float advanceSpeed;
    public float runSpeed;
    public float jumpSpeed;
    public float jumpTime;
    public float fallSpeed;
    public float airMoveSpeed;
    
    //will be discuss later
    public PlayerState state;
    
    void Start()
    {
        hasSword = true;
        isGrounded = true; // change when refining game feel (players jump in)
        isDowned = false;
        isDead = false;

        //ADD FINDING THE CHILD COLLIDERS HERE
        punchColliderTag = punchColliderObject.GetComponent<ColliderTag>();
        punchCollider = punchColliderObject.GetComponent<BoxCollider2D>();


        animator = gameObject.GetComponent<Animator>();
        //Debug.Log(animator);
        playerInput = GetComponent<PlayerInput>();

        //ResetState();Somebody says pointless, blame him . :(
        state = ChangeState(new UnarmedStillStandingState(this, animator));
        
        GameObject.Find("Camera Object").GetComponent<CameraScript>().AddActivePlayer(gameObject);
        //Debug.Log(GameObject.Find("Camera Object").GetComponent<CameraScript>().activePlayers.Count);
    }
    
    void Update()
    {
        // Handle Input
        int moveDir = 0;
        //bool isMoving = false;
        if (Input.GetKey(KeyCode.RightArrow))
            moveDir += 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            moveDir -= 1;
        if (moveDir != 0)
            //isMoving = true;
        //animator?.SetBool("IsMoving", isMoving);
        
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
        Debug.Log(state);
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
