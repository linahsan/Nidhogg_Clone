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
    public InputAction debug;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        right = playerInput.actions.FindAction("Right");
        left = playerInput.actions.FindAction("Left");
        up = playerInput.actions.FindAction("Up");
        down = playerInput.actions.FindAction("Down");
        jump = playerInput.actions.FindAction("Jump");
        attack = playerInput.actions.FindAction("Attack");
        debug = playerInput.actions.FindAction("Debug");
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
    public bool RightPressed() => right.IsPressed();

    public bool RightReleased() => right.WasReleasedThisFrame();
    //left:
    public bool LeftPressed() => left.IsPressed();

    public bool LeftReleased() => left.WasReleasedThisFrame();
    //up:
    public bool UpPressed() => up.WasPressedThisFrame();

    public bool UpReleased() => up.WasReleasedThisFrame();
    //down:
    public bool DownPressed() => down.WasPressedThisFrame();
    
    public bool DownPressedLong() => down.IsPressed();

    public bool DownReleased() => down.WasReleasedThisFrame();
    //jump:
    public bool JumpPressed() => jump.IsPressed();

    public bool JumpReleased() => jump.WasReleasedThisFrame();
    //attack:
    public bool AttackPressed() => attack.IsPressed();

    public bool AttackReleased() => attack.WasReleasedThisFrame();

    public bool DebugPressed() => debug.IsPressed();

    public bool DebugReleased() => debug.WasReleasedThisFrame();
    
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
