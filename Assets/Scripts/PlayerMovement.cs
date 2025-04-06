using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float enGardeSpeed;
    [SerializeField] PlayerInputScript playerInputScript;
    
    void Start()
    {
        enGardeSpeed = 5f;
        playerInputScript = GetComponent<PlayerInputScript>();
    }

    
    void Update()
    {
        float move = playerInputScript.moveDirection;
        move *= enGardeSpeed;
        transform.Translate(Vector2.right * move * Time.deltaTime);
    }
}
