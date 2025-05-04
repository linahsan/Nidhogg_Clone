using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInputScript input;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CameraScript cameraScript;
    [SerializeField] PlayerMovement playerMovement;
    
    public float moveSpeed = 5f;
    public bool isJumping = false;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float gravity = -30f;
    [SerializeField] private float fallMultiplier = 1.5f;
    [SerializeField] float verticalVelocity;
    public bool isGrounded = false;
    public bool isFalling;
    public bool isPlayer1;
    public bool isAlive = true;
    public bool isCrouching;
    //respawn code:
    public int respawnTime;
    private int deathTimer;
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
        playerMovement = GetComponent<PlayerMovement>();
        
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

        cameraScript = GameObject.FindWithTag("Camera").GetComponent<CameraScript>();
        cameraScript.AddActivePlayer(gameObject);
        cameraScript.Initalization();

       
        
    }

    void FixedUpdate()
    {
        if(isAlive)
        {
            CheckGrounded();
            ApplyGravity();
            HandleMovement();
            HandleJump();
            //HandleCameraEdges();
            HandleCrouch();
        }
        else
        {
            deathTimer++;
            transform.position = new Vector3(-20, -20, 0);
            if(deathTimer > respawnTime)
            {
                PlayerRespawns();
            }
        }
        
        if(input.DebugPressed() && !hasDied)
        {
            PlayerDies();
        }


    }

    void OnBecameInvisible()
    {
        PlayerDies();
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
        isGrounded = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            //other possible if statement: hit.transform.gameObject.tag == LayerMask.NameToLayer("Ground")
            if (hit.transform.gameObject.tag == "Ground" && !isJumping)
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

    void HandleCrouch()
    {

        if (playerMovement.currentHeight == 0 && input.DownPressedLong() && isGrounded && !input.DownPressed())
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }
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

    public void GetOtherPlayerVariables()
    {
        otherPlayer = GetOtherPlayer();
        otherPlayerTransform = otherPlayer.GetComponent<Transform>();
    }

    public void HandleCameraEdges()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        if(isPlayer1)
        {
            if(transform.position.x < cameraScript.gameObject.GetComponent<Transform>().position.x - (1/2)*width)
            {
                transform.position = new Vector3(cameraScript.gameObject.GetComponent<Transform>().position.x - (1/2)*width, transform.position.y, 0);
            }
        }
        else
        {
            if(transform.position.x > cameraScript.gameObject.GetComponent<Transform>().position.x + (1/2)*width)
            {
                transform.position = new Vector3(cameraScript.gameObject.GetComponent<Transform>().position.x + (1/2)*width, transform.position.y, 0);
            }
        }
    }

    public void PlayerDies()
    {
        if(isAlive)
        {
            isAlive = false;
            cameraScript.PlayerDies(gameObject);
            deathTimer = 0;
            isCrouching = false;
            isFalling = false;
            //its *possible* I may need to mess w "isFacingDefaultDirection" here
        }
    }

    public void PlayerRespawns()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        isAlive = true;
        //SET ANIMATOR TO RUNNing state

        //WILL NEED TO FIX THE Y VALUE HERE
        if(isPlayer1)
        {
            gameObject.transform.position = new Vector3(cam.transform.position.x - (1/2)*width, 2, 0);
        }
        else
        {
            gameObject.transform.position = new Vector3(cam.transform.position.x + (1/2)*width, 2, 0);
        }
        
    }
}
