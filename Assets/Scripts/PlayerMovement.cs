using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float enGardeSpeed;
    [SerializeField] PlayerInputScript playerInputScript;
    
    [SerializeField] float jumpSpeed;
    
    void Start()
    {
        //move
        enGardeSpeed = 5f;
        playerInputScript = GetComponent<PlayerInputScript>();
        
        //jump
        jumpSpeed = 5f;
    }

    
    void Update()
    {
        //move
        float move = playerInputScript.moveDirection;
        move *= enGardeSpeed;
        transform.Translate(Vector2.right * move * Time.deltaTime);
        
        //jump
        float jump = playerInputScript.jumpDirection;
        jump *= jumpSpeed;
        transform.Translate(Vector2.up * jump * Time.deltaTime);
    }
}
