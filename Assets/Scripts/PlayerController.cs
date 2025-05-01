using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInputScript input;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    
    public float moveSpeed = 5f;
    [SerializeField] bool isJumping = false;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float gravity = -30f;
    [SerializeField] private float fallMultiplier = 1.5f;
    [SerializeField] float verticalVelocity;
    [SerializeField] bool isGrounded = false;
    public bool isPlayer1;
    
    void Start()
    {
        input = GetComponent<PlayerInputScript>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        isGrounded = false;
        
        if (this.name == "Player1")
        {
            isPlayer1 = true;
            spriteRenderer.flipX = false;
        }
        else
        {
            isPlayer1 = false;
            spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        CheckGrounded();
        ApplyGravity();
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        Vector2 direction = Vector2.zero;
        
        if (input.LeftPressed())
        {
            direction.x = -1f;

            if (isPlayer1)
            {
                spriteRenderer.flipX = true;
            }
        }
        else if (input.RightPressed())
        {
            direction.x = 1f;
            
            if (!isPlayer1)
            {
                spriteRenderer.flipX = false;
            }
        } 
        else if (input.LeftReleased() && isPlayer1)
        {
            spriteRenderer.flipX = false;
        } 
        else if (input.RightReleased() && !isPlayer1)
        {
            spriteRenderer.flipX = true;
        }

        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void HandleJump()
    {
        if (input.JumpPressed() && isGrounded)
        {
            verticalVelocity = jumpForce;
            isJumping = true;
            isGrounded = false;
        }
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            float currentGravity = gravity;

            if (verticalVelocity < 0)
            {
                currentGravity *= fallMultiplier;
            }

            verticalVelocity += currentGravity * Time.deltaTime;
            transform.Translate(new Vector2(0, verticalVelocity * Time.deltaTime));
        }
        else
        {
            verticalVelocity = 0f;
        }
    }

    void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            isGrounded = true;
            verticalVelocity = 0f;

            // snapping ot ground --> might not need, need to check iwht high heights
            float groundY = hit.point.y + GetComponent<Collider2D>().bounds.extents.y;
            
            Vector2 pos = transform.position;
            pos.y = groundY;
            transform.position = pos;
        }
    }
}