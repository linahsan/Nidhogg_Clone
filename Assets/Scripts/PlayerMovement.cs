using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// THIS IS A TEMPORARY IMPLEMENTATION SO IGNORE BADLY WRITTEN STUFF - lina
    /// </summary>
    [SerializeField] float enGardeSpeed;
    
    void Start()
    {
        enGardeSpeed = 5f;
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
