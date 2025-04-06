using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    public float moveDirection;
    public float jumpDirection;
    [SerializeField] PlayerInput playerInput;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        //Move
        playerInput.actions["Move (En Garde)"].performed += OnMove;
        playerInput.actions["Move (En Garde)"].canceled += OnMove;
        
        //Jump
        playerInput.actions["Jump"].performed += OnJump;
        playerInput.actions["Jump"].canceled += OnJump;
    }
    
    private void OnDisable()
    {
        //Move
        playerInput.actions["Move (En Garde)"].performed -= OnMove;
        playerInput.actions["Move (En Garde)"].canceled -= OnMove;
        
        //Jump
        playerInput.actions["Jump"].performed -= OnJump;
        playerInput.actions["Jump"].canceled -= OnJump;
    }
    
    private void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>().x;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        jumpDirection = context.ReadValue<Vector2>().y;
    }
}
