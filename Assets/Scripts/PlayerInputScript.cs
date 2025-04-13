using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    public float moveDirection;
    public float jumpDirection;
    [SerializeField] PlayerInput playerInput;
    public InputAction right;
    public InputAction left;
    public InputAction up;
    public InputAction down;
    public InputAction jump;
    public InputAction attack;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        right = playerInput.actions.FindAction("Right");
        left = playerInput.actions.FindAction("Left");
        up = playerInput.actions.FindAction("Up");
        down = playerInput.actions.FindAction("Down");
        jump = playerInput.actions.FindAction("Jump");
        attack = playerInput.actions.FindAction("Attack");
    }

    private void OnEnable()
    {
        right.Enable();
        left.Enable();
        up.Enable();
        down.Enable();
        jump.Enable();
        attack.Enable();
        /*
        //Move
        playerInput.actions["Move (En Garde)"].performed += OnMove;
        playerInput.actions["Move (En Garde)"].canceled += OnMove;
        
        //Jump
        playerInput.actions["Jump"].performed += OnJump;
        playerInput.actions["Jump"].canceled += OnJump;
        */
    }
    
    private void OnDisable()
    {
        right.Disable();
        left.Disable();
        up.Disable();
        down.Disable();
        jump.Disable();
        attack.Disable();
        /*
        //Move
        playerInput.actions["Move (En Garde)"].performed -= OnMove;
        playerInput.actions["Move (En Garde)"].canceled -= OnMove;
        
        //Jump
        playerInput.actions["Jump"].performed -= OnJump;
        playerInput.actions["Jump"].canceled -= OnJump;
        */
    }
    //right:
    public bool RightPressed()
    {
        if(right.WasPressedThisFrame())
        {
            return true;
        }
        return false;
    }

    public bool RightReleased()
    {
        if(right.WasReleasedThisFrame())
        {
            return true;
        }
        return false;
    }
    //left:
    public bool LeftPressed()
    {
        if(left.WasPressedThisFrame())
        {
            return true;
        }
        return false;
    }

    public bool LeftReleased()
    {
        if(left.WasReleasedThisFrame())
        {
            return true;
        }
        return false;
    }
    //up:
    public bool UpPressed()
    {
        if(up.WasPressedThisFrame())
        {
            return true;
        }
        return false;
    }

    public bool UpReleased()
    {
        if(up.WasReleasedThisFrame())
        {
            return true;
        }
        return false;
    }
    //down:
    public bool DownPressed()
    {
        if(down.WasPressedThisFrame())
        {
            return true;
        }
        return false;
    }

    public bool DownReleased()
    {
        if(down.WasReleasedThisFrame())
        {
            return true;
        }
        return false;
    }
    //jump:
    public bool JumpPressed()
    {
        if(jump.WasPressedThisFrame())
        {
            return true;
        }
        return false;
    }

    public bool JumpReleased()
    {
        if(jump.WasReleasedThisFrame())
        {
            return true;
        }
        return false;
    }
    //attack:
    public bool AttackPressed()
    {
        if(attack.WasPressedThisFrame())
        {
            return true;
        }
        return false;
    }

    public bool AttackReleased()
    {
        if(attack.WasReleasedThisFrame())
        {
            return true;
        }
        return false;
    }

    /*
    private void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>().x;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        jumpDirection = context.ReadValue<Vector2>().y;
    }
    */
}
