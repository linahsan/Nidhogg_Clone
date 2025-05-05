using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SwordScript sword;
    [SerializeField] PlayerInputScript input;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CameraScript cameraScript;
    [SerializeField] PlayerMovement playerMovement;

    public float runSpeed = 5f;
    public float moveSpeed = 5f;
    public bool isJumping = false;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float normalJumpForce = 15f;
    [SerializeField] private float crouchJumpForce = 15f;
    [SerializeField] private float gravity = -30f;
    [SerializeField] private float fallMultiplier = 1.5f;
    [SerializeField] float verticalVelocity;
    public bool isGrounded = false;
    public bool isFalling;
    public bool isPlayer1;
    public bool isAlive = true;
    public bool isCrouching;
    public bool isRolling;
    public float crawlSpeed = 3f;
    public float rollSpeed = 5f;
    public bool isDying = false;
    [SerializeField] private float rollFriction = 0.00005f;
    public float currentRollSpeed = 0f;
    [SerializeField] private float minSpeedToCrawl = 0.2f;

    public bool isAttacking;
    //respawn code:
    public int respawnTime;
    public int deathTimer;

    // flip logic
    private bool facingDefault;
    private bool defaultFacingRight;

    //orietnation:
    private GameObject otherPlayer;
    private Transform otherPlayerTransform;
    private int orientation = 1;

    //hitbox
    public Collider2D grabChild;
    [SerializeField] private Collider2D crouchCollider;
    [SerializeField] private Collider2D headCollider;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private Collider2D bottomCollider;
    
    //public GameObject grabChild;
    
    [SerializeField] float wallCheckDistance = 1f;
    [SerializeField] bool isTouchingWall = false;
    [SerializeField] bool isWallClinging = false;
    public Transform ledgeCornerCheck;
    public float ledgeCheckDistance = 0.1f;
    public GameObject bloodSplatter;
    public LayerMask wallLayer;

    void Start()
    {
        input = GetComponent<PlayerInputScript>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();

        grabChild = bottomCollider;

        isGrounded = false;

        var layer1 = LayerMask.NameToLayer("Player 1 Hit Box");
        var layer2 = LayerMask.NameToLayer("Player 2 Hit Box");
        if (this.name == "Player1")
        {
            isPlayer1 = true;
            defaultFacingRight = true;
            headCollider.includeLayers = layer1;
            bodyCollider.includeLayers = layer1;
            bottomCollider.includeLayers = layer1;
            crouchCollider.includeLayers = layer1;
            sword.SetOwner(SwordScript.SwordOwner.Player1);
        }
        else
        {
            isPlayer1 = false;
            defaultFacingRight = false;
            headCollider.includeLayers = layer2;
            bodyCollider.includeLayers = layer2;
            bottomCollider.includeLayers = layer2;
            crouchCollider.includeLayers = layer2;
            sword.SetOwner(SwordScript.SwordOwner.Player2);
        }

        facingDefault = true;
        SetSpriteFacing(defaultFacingRight);

        cameraScript = GameObject.FindWithTag("Camera").GetComponent<CameraScript>();
        cameraScript.AddActivePlayer(gameObject);
        cameraScript.Initalization();

       OnEnterScene();

    }

    void Update()
    {
        if (!isAttacking && input.AttackPressed())
        {
            animator.SetBool("IsAttacking", true);
            isAttacking = true;
        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                animator.SetBool("IsAttacking", false);
            isAttacking = false;
        }

        if (isAttacking && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            PlayerDies();
        }
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            ApplyGravity();
            CheckGrounded();
            CheckWall();
            
            HandleMovement();
            HandleJump();
            //HandleCameraEdges();
            HandleCrouch();
            HandleAttack();
            HandleRoll();
        }
        else
        {
            deathTimer++;
            transform.position = new Vector3(-20, -20, 0);
            if (deathTimer > respawnTime)
            {
                PlayerRespawns();
            }
        }

        if (input.DebugPressed())
        {
            PlayerDies();
            Debug.Log("Debug Pressed");
        }


    }

    void OnBecameInvisible()
    {
        PlayerDies();
    }

    void HandleMovement()
    {
        Vector2 direction = Vector2.zero;

        if (playerMovement.currentAnimation == "Armed_Crawling")
        {
            moveSpeed = crawlSpeed;
        }
        else if (playerMovement.currentAnimation == "Armed_Rolling")
        {
            moveSpeed = rollSpeed;
        }
        else if (playerMovement.isSliding)
        {
            moveSpeed = 2f;
        }
        else
        {
            moveSpeed = runSpeed;
        }

        if (!playerMovement.retreatPlay)
        {
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
        }

        if (playerMovement.currentAnimation == "Armed_Lunged_Mid")
        {
            Debug.Log("attack");
        }

        if (!input.AttackPressed() ||
            playerMovement.currentAnimation != "Armed_Lunged_Mid" ||
            playerMovement.currentAnimation != "Armed_Lunge_High" ||
            playerMovement.currentAnimation != "Armed_Lunge_Low")
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
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
                float groundY = hit.point.y + grabChild.bounds.extents.y + grabChild.bounds.size.y/2;


                Vector2 pos = transform.position;
                pos.y = groundY;
                transform.position = pos;
            }

        }
    }

    void Flip()
    {
        if (playerMovement.retreatPlay) return;
        facingDefault = !facingDefault;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        //spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    void SetSpriteFacing(bool faceRight)
    {
        //spriteRenderer.flipX = !faceRight;
        if (!faceRight)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }


    public bool IsAlive()
    {
        return isAlive;
    }

    void HandleCrouch()
    {

        if (playerMovement.currentHeight == 0 && input.DownPressedLong() && !input.DownPressed())
        {

            isCrouching = true;
            bottomCollider.gameObject.SetActive(false);
            headCollider.gameObject.SetActive(false);
            bodyCollider.gameObject.SetActive(false);  
            crouchCollider.gameObject.SetActive(true);
            Debug.Log("We are currently crouching");

        }
        else
        {
            isCrouching = false;
            bottomCollider.gameObject.SetActive(true);
            headCollider.gameObject.SetActive(true);
            bodyCollider.gameObject.SetActive(true);   
            crouchCollider.gameObject.SetActive(false);
        }
    }

    void HandleRoll()
    {
        if (playerMovement.currentAnimation == "Armed_Rolling")
        {
            animator.SetBool("IsRolling", true);
            animator.speed = currentRollSpeed / rollSpeed;
            currentRollSpeed = Mathf.Max(0f, currentRollSpeed - rollFriction * Time.deltaTime);
            animator.SetFloat("RollingSpeed", currentRollSpeed);
            
            if (currentRollSpeed <= minSpeedToCrawl)
            {
                isRolling = false;
                animator.speed = 1f;
                animator.SetBool("IsRolling", false);
            }
        }
        else
        {
            animator.speed = 1f;
            currentRollSpeed = rollSpeed;
            animator.SetFloat("RollingSpeed", 0f);
        }
        
    }

    void HandleAttack()
    {
        if (input.AttackPressed())
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }
    
    
    void CheckWall()
    {
        Vector2 wallDirection;
        if (transform.localScale.x > 0)
        {
            wallDirection = Vector2.right;
        }
        else
        {
            wallDirection = Vector2.left;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, wallDirection, wallCheckDistance, wallLayer);
        Debug.DrawRay(transform.position, wallDirection * wallCheckDistance, Color.green);
        
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            isTouchingWall = true;
            Debug.Log("istouchingwall");
            float playerBounds = GetComponent<SpriteRenderer>().bounds.extents.x;
            float wallX = hit.point.x;
            if (transform.localScale.x > 0)
            {
                transform.position = new Vector2(wallX - playerBounds, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(wallX + playerBounds, transform.position.y);
            }
        }
        else
        {
            isTouchingWall = false;
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
        if (isPlayer1)
        {
            if (transform.position.x < cameraScript.gameObject.GetComponent<Transform>().position.x - (1 / 2) * width)
            {
                transform.position = new Vector3(cameraScript.gameObject.GetComponent<Transform>().position.x - (1 / 2) * width, transform.position.y, 0);
            }
        }
        else
        {
            if (transform.position.x > cameraScript.gameObject.GetComponent<Transform>().position.x + (1 / 2) * width)
            {
                transform.position = new Vector3(cameraScript.gameObject.GetComponent<Transform>().position.x + (1 / 2) * width, transform.position.y, 0);
            }
        }
    }


    public void PlayerDies()
    {
        if(isAlive)
        {
            isAlive = false;
            //Debug.Log(isAlive);
            cameraScript.PlayerDies(gameObject);
            deathTimer = 0;
            isCrouching = false;
            isFalling = false;
            //Debug.Log("happened");
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

        if (isPlayer1)
        {
            gameObject.transform.position = new Vector3(cam.transform.position.x - (1 / 2) * width, 2, 0);
        }
        else
        {
            gameObject.transform.position = new Vector3(cam.transform.position.x + (1 / 2) * width, 2, 0);
        }
        
        animator.SetTrigger("Death");
    }


    public void OnEnterScene()
    {
        if(SceneTransitionManager.Instance.winningDirection == 1)
        {
            if(isPlayer1)
            {
                transform.position = new Vector3(SceneTransitionManager.Instance.playerSpawnX, SceneTransitionManager.Instance.playerSpawnY, 0);
            }
            else
            {
                PlayerDies();
            }
        }
        else if(SceneTransitionManager.Instance.winningDirection == -1)
        {
            if(isPlayer1)
            {
                PlayerDies();
            }
            else
            {
                transform.position = new Vector3(SceneTransitionManager.Instance.playerSpawnX, SceneTransitionManager.Instance.playerSpawnY, 0);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<DoorScript>())
        {
            collision.gameObject.GetComponent<DoorScript>().DoorSceneChange();
        }
    }

    public void HandleDeath(BoxCollider2D hittenBox)
    {
        if (hittenBox == headCollider)
            animator.SetBool("HitByHead", true);
        else if (hittenBox == bodyCollider)
            animator.SetBool("HitByBody", true);
        else if (hittenBox == bottomCollider)
            animator.SetBool("HitByBottom", true);
        else if (hittenBox == crouchCollider)
            animator.SetBool("HitByCrouch", true);
        
        isDying = true; 
        
        if(hittenBox.gameObject.GetComponent<DoorScript>())
        {
            hittenBox.gameObject.GetComponent<DoorScript>().DoorSceneChange();
        }
    }
}

