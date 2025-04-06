using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    public float moveDirection;
    [SerializeField] PlayerInput playerInput;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerInput.actions["Move (En Garde)"].performed += OnMove;
        playerInput.actions["Move (En Garde)"].canceled += OnMove;
    }
    
    private void OnDisable()
    {
        playerInput.actions["Move (En Garde)"].performed -= OnMove;
        playerInput.actions["Move (En Garde)"].canceled -= OnMove;
    }
    
    private void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>().x;
    }
}
