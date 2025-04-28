using UnityEngine;

public class ColliderTag : MonoBehaviour
{
    public enum colliderType
    {
        BODY,
        FIST
    }

    public colliderType type;

    public GameObject collidingWith;

    public bool isColliding;

    void OnTriggerEnter2D(Collider2D collision)
    {
        isColliding = true;
        
    }
}
