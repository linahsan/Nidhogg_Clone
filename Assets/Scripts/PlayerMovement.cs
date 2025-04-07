using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float enGardeSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] PlayerInputScript playerInputScript; 
    [SerializeField] PlayerManger playerManger;

    //for jump
    float currentJumpSpeed;
    [SerializeField] float gravity;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] float maxRunningSpeed;
    [SerializeField] float crouchMoveSpeed;
    private float currentMoveSpeed;
    private Vector3 currentVelocity;
    
    void Start()
    {
        playerInputScript = GetComponent<PlayerInputScript>();
        //move
        enGardeSpeed = 5f;
        
        //jump
        jumpSpeed = 5f;
        gravity = 0.98f;
        
        // Subscribe to event
        playerManger.OnPlayerStateEnds += OnPlayerStateEnd;
        playerManger.OnPlayerStateStart += OnPlayerStateStart;
    }
    
    //unsubscribe to event
    private void OnDestroy()
    {
        playerManger.OnPlayerStateEnds -= OnPlayerStateEnd;
        playerManger.OnPlayerStateStart -= OnPlayerStateStart;
    }


    void Update()
    {
        //previous implementation
        /*
        //move
        float move = playerInputScript.moveDirection;
        move *= enGardeSpeed;
        transform.Translate(Vector2.right * move * Time.deltaTime);
        
        //jump
        float jump = playerInputScript.jumpDirection;
        jump *= jumpSpeed;
        transform.Translate(Vector2.up * jump * Time.deltaTime);
        transform.Translate(Vector2.down * gravity * Time.deltaTime);
        */
       
        //falling
        currentVelocity.y = Mathf.Max(maxFallSpeed, currentVelocity.y - gravity * Time.deltaTime);
        //movement

    }
    
    //switch the player state here
    private void OnPlayerStateStart(PlayerManger.PlayerStates newState)
    {
        if (newState == PlayerManger.PlayerStates.JUMPING)
        {
            
        }
    }

    private void OnPlayerStateEnd(PlayerManger.PlayerStates previousState)
    {
        
    }


    private void StartJump()
    {
        currentVelocity.y = currentJumpSpeed;
    }
}
