using Unity.Jobs;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] PlayerInputScript input; 
    [SerializeField] Animator animator;
    
    // Standing Idle Heights
    private int currentHeight = 1;
    
    // speed
    private bool movingForward = false;
    private bool stepBack = false;
    
    void Start()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateControllerBools();
        HeightCheck();
        MoveForwardCheck();
        JumpingCheck();
        FallingCheck();
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

    void UpdateControllerBools()
    {
        animator.SetBool("isFalling", controller.isFalling);
        animator.SetBool("isJumping", controller.isJumping);
        animator.SetBool("isGrounded", controller.isGrounded);
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
}
