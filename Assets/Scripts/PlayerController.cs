using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInputScript input;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CameraScript cameraScript;
    
    public float moveSpeed = 5f;
    public bool isJumping = false;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float gravity = -30f;
    [SerializeField] private float fallMultiplier = 1.5f;
    [SerializeField] float verticalVelocity;
    public bool isGrounded = false;
    public bool isFalling;
    public bool isPlayer1;
    public bool isAlive =true;
    
    // flip logic
    private bool facingDefault;
    private bool defaultFacingRight;

    //debugging:
    private bool hasDied = false;
    
    //orietnation:
    private GameObject otherPlayer;
    private Transform otherPlayerTransform;
    private int orientation = 1;
    
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
        cameraScript.AddActivePlayer(gameObject);
        cameraScript.Initalization();

        otherPlayer = GetOtherPlayer();
        otherPlayerTransform = otherPlayer.GetComponent<Transform>();
        
    }

    void FixedUpdate()
    {
        CheckGrounded();
        ApplyGravity();
        HandleMovement();
        HandleJump();
        
        if(input.DebugPressed() && !hasDied)
        {
            isAlive = !isAlive;
            cameraScript.PlayerDies(gameObject);
            hasDied = true;
        }

        if(input.DebugReleased())
        {
            hasDied = false;
        }
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
    

    public bool IsAlive()
    {
        return isAlive;
    }

    //handling orientation functions
    public GameObject GetOtherPlayer()
    {
        GameObject otherPlayer = null;
        for (int i = 0; i < cameraScript.activePlayers.Count; i++)
        {
            if (cameraScript.activePlayers[i].GetComponent<PlayerController>().isPlayer1 != isPlayer1)
            {
                otherPlayer = cameraScript.activePlayers[i];
            }
        }
        return otherPlayer;
    }

    public void HandleOrientation()
    {
        
        float distance = otherPlayerTransform.position.x - transform.position.x;
        orientation = (int)Mathf.Sign(distance);
        //transform.localScale = new Vector3(orientation, transform.localScale.y, transform.localScale.z);
        
        
    }
}