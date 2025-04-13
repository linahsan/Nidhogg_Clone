using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool hasSword;
    public bool isGrounded;
    public bool isDowned;
    public bool isDead;
    void Start()
    {
        hasSword = true;
        isGrounded = true; // change when refining game feel (players jump in)
        isDowned = false;
        isDead = false;
        ResetState();
    }
    
    void Update()
    {
        if (isDead)
        {
            
        }

        if (isDowned)
        {
            
        }

        if (hasSword)
        {
            
        }
        else
        {
            
        }
        
    }

    void ResetState()
    {
        if (hasSword)
        {
            
        }
        else
        {
            
        }
    }
}
