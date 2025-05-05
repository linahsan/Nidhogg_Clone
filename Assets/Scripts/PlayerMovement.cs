using Unity.Jobs;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] PlayerInputScript input; 
    [SerializeField] Animator animator;
    public string currentAnimation;
    
    // Standing Idle Heights
    public int currentHeight = 1;
    
    // speed
    [SerializeField] bool movingForward = false;
    [SerializeField] bool stepBack = false;
    [SerializeField] bool isCrouching = false;
    
    private SpriteRenderer _swordSpriteRenderer;
    public int SwordSpriteOrderInLayer;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInputScript>();
        _swordSpriteRenderer = transform.Find("Sword").GetComponent<SpriteRenderer>();
        SwordSpriteOrderInLayer = _swordSpriteRenderer.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        currentAnimation = this.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        UpdateControllerBools();
        HeightCheck();
        MoveForwardCheck();
        JumpingCheck();
        FallingCheck();
        CrouchingCheck();
        CrawlingCheck();
        RollingCheck();
        _swordSpriteRenderer.sortingOrder = SwordSpriteOrderInLayer;
    }

    // 
    void HeightCheck()
    {
        if (input.DownPressed() && animator.GetInteger("height") > 0)
        {
            currentHeight--;
            UpdateHeightAnimation();
        }
        else if (input.UpPressed() && animator.GetInteger("height") < 2)
        {
            currentHeight++;
            UpdateHeightAnimation();
        }
    }
    
    void MoveForwardCheck()
    {
        if (input.LeftPressed())
        {
            movingForward = true;

            if (controller.isPlayer1)
            {
                stepBack = true;
            }
            
            UpdateMoveForwardAnimation();
        }
        else if (input.RightPressed())
        {
            movingForward = true;

            if (!controller.isPlayer1)
            {
                stepBack = true;
            }
            UpdateMoveForwardAnimation();
        }
        if(input.LeftReleased() || input.RightReleased())
        {
            movingForward = false;
            stepBack = false;
            
            UpdateMoveForwardAnimation();
        }
    }

    void JumpingCheck()
    {
        if (input.JumpPressed())
        {
            UpdateJumpAnimation();
        }
    }

    void FallingCheck()
    {
        if (controller.isFalling)
        {
            UpdateFallAnimation();
        }
    }

    void CrouchingCheck()
    {
        if (currentHeight == 0 && input.DownPressedLong() && controller.isGrounded && !input.DownPressed())
        {
            isCrouching = true;
            UpdateCrouchAnimation();
        }
        else
        {
            isCrouching = false;
        }
    }

    void CrawlingCheck()
    {
        if (controller.isCrouching)
        {
            Debug.Log("is crouching");
            if (currentAnimation == "Armed_Crawling")
            {
                Debug.Log("hi");
                if (!movingForward)
                {
                    Debug.Log("stop crouching");
                    animator.speed = 0f;
                }
                else
                {
                    animator.speed = 1f;
                }
            }
        }
        else
        {
            animator.speed = 1f;
        }
    }

    void RollingCheck()
    {
        if (controller.isCrouching && movingForward)
        {
            UpdateRollAnimation();
        }
    }

    void UpdateControllerBools()
    {
        animator.SetBool("isFalling", controller.isFalling);
        animator.SetBool("isJumping", controller.isJumping);
        animator.SetBool("isGrounded", controller.isGrounded);
        animator.SetBool("isCrouching", isCrouching);
    }

    // update height animation
    void UpdateHeightAnimation()
    {
        animator.SetInteger("height", currentHeight);
        animator.SetBool("movingForward", movingForward);
        animator.SetBool("stepBack", stepBack);
    }

    // update animation to step forward or step back
    void UpdateMoveForwardAnimation()
    {
        animator.SetInteger("height", currentHeight);
        animator.SetBool("movingForward", movingForward);
        animator.SetBool("stepBack", stepBack);
    }

    void UpdateJumpAnimation()
    {
    }

    void UpdateFallAnimation()
    {
    }

    void UpdateCrouchAnimation()
    {
        animator.SetInteger("height", currentHeight);
    }

    void UpdateRollAnimation()
    {
        animator.SetInteger("height", currentHeight);
        animator.SetBool("movingForward", movingForward);
    }
}
