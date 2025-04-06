using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// THIS IS A TEMPORARY IMPLEMENTATION SO IGNORE BADLY WRITTEN STUFF - lina
    /// </summary>
    [SerializeField] float enGardeSpeed;
    [SerializeField] Vector2 enGardeInput;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerInput playerInput;
    
    void Start()
    {
        enGardeSpeed = 5f;
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            
        }
    }
}
