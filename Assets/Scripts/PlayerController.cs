using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInputScript input;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    
    public float moveSpeed = 5f;
    public bool isJumping = false;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float gravity = -30f;
    [SerializeField] private float fallMultiplier = 1.5f;
    [SerializeField] float verticalVelocity;
    public bool isGrounded = false;
    public bool isFalling;
    public bool isPlayer1;
    
    // flip logic
    private bool facingDefault;
    private bool defaultFacingRight;
    
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
            defaultFacingRight = true;
        }
        else
        {
            isPlayer1 = false;
            defaultFacingRight = false;
        }
        
        facingDefault = true;
        SetSpriteFacing(defaultFacingRight);
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

            if (isPlayer1 && facingDefault)
            {
                Flip();
            }
            else if (!isPlayer1 && !facingDefault)
            {
                Flip();
            }
        }
        else if (input.RightPressed())
        {
            direction.x = 1f;
            
            if (!isPlayer1 && facingDefault)
            {
                Flip();
            }
            else if (isPlayer1 && !facingDefault)
            {
                Flip();
            }
        }
        else
        {
            if (!facingDefault)
            {
                Flip();
            }
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
            isFalling = false;
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
                isFalling = true;
                isJumping = false;
            }
            else
            {
                isFalling = false;
            }

            verticalVelocity += currentGravity * Time.deltaTime;
            transform.Translate(new Vector2(0, verticalVelocity * Time.deltaTime));
        }
        else
        {
            verticalVelocity = 0f;
            isFalling = false;
        }
    }

    void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            isGrounded = true;
            isFalling = false;
            isJumping = false;
            verticalVelocity = 0f;

            // snapping ot ground --> might not need, need to check iwht high heights
            float groundY = hit.point.y + GetComponent<Collider2D>().bounds.extents.y;
            
            Vector2 pos = transform.position;
            pos.y = groundY;
            transform.position = pos;
        }
    }
    
    void Flip()
    {
        facingDefault = !facingDefault;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
    
    void SetSpriteFacing(bool faceRight)
    {
        spriteRenderer.flipX = !faceRight;
    }
}