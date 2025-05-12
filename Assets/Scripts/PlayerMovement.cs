using System;
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
    public bool movingForward = false;
    public bool isRunning = false;
    public bool isIdle = false;
    public bool isStepping = false;
    
    [SerializeField] bool stepBack = false;
    [SerializeField] bool stepForward = false;
    public bool isCrouching = false;
    
    
    public bool retreatPlay = false;
    public bool isSliding = false;
    
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
        UpdateControllerBools();
        StepBackCheck();
        HeightCheck();
        MoveForwardCheck();
        JumpingCheck();
        FallingCheck();
        CrouchingCheck();
        try
        {
            CrawlingCheck();
        }
        catch (Exception e)
        {
        }

        RollingCheck();
        SlideCheck();
        _swordSpriteRenderer.sortingOrder = SwordSpriteOrderInLayer;
    }

    // 
    void HeightCheck()
    {
        bool isrunning =  animator.GetCurrentAnimatorStateInfo(0).IsName("Armed_Running");

        if (isrunning)
        {
            //downpress is rolling, uppress is rasing sword
        }
        else
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
        if (currentHeight == 0 && input.DownPressedLong() && !input.DownPressed())
        {
            /*
            if(isCrouching == false)
            {
                animator.Play("Armed_Crouching", 0, 0);
            }
            */
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
        
        currentAnimation = this.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
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

    void StepBackCheck()
    {
        if (stepBack && (input.RightPressedShort() || input.LeftPressedShort()))
        {
            Debug.Log("step back");
            retreatPlay = true;
        }
        else
        {
            retreatPlay = false;
        }
    }

    void SlideCheck()
    {
        if (input.LeftPressed() && input.RightPressed())
        {
            isSliding = true;
        }
        else
        {
            isSliding = false;
        }
    }

    void AttackCheck()
    {
        if (input.AttackPressed())
        {
            UpdateAttackAnimation();
        }
    }
    void UpdateControllerBools()
    {
        animator.SetBool("isFalling", controller.isFalling);
        animator.SetBool("isJumping", controller.isJumping);
        animator.SetBool("isGrounded", controller.isGrounded);
        animator.SetBool("isCrouching", isCrouching);
        //animator.SetBool("isSliding", isSliding);
        animator.SetBool("isAttacking", controller.isAttacking);
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

    void UpdateAttackAnimation()
    {
        
    }
}
