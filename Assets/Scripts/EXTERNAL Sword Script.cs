using UnityEngine;

public class EXTERNALSwordScript : MonoBehaviour
{
    public enum STATES
    {
        IN_AIR,
        FALLING,
        ON_GROUND
    }
    public STATES state = STATES.IN_AIR;
    public int directionValue;
    public float speed;
    public bool thrownPlayer;
    public Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATES.IN_AIR:
                gameObject.transform.Translate(new Vector3 (directionValue * speed, 0, 0));
                animator.SetTrigger("isSpinning");
                break;

            case STATES.FALLING:
                gameObject.transform.Translate(new Vector3 (0, -speed, 0));
                animator.SetTrigger("isSpinning");
                break;
            
            case STATES.ON_GROUND:
                animator.SetTrigger("isStill");
                break;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(state == STATES.IN_AIR && collision.gameObject.tag != "Ground") //&& collision.gameObject.tag != "Player")
        {
            state = STATES.FALLING;
        }
        else if(state == STATES.FALLING && collision.gameObject.tag == "Ground")
        {
            state = STATES.ON_GROUND;
            float collisionY = collision.gameObject.transform.position.y;
            BoxCollider2D collisionCollider = collision.gameObject.GetComponent<BoxCollider2D>();

            transform.position = new Vector3(transform.position.x, collisionY + collisionCollider.bounds.extents.y, 0);

            

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Happened");

        bool willHappen = false;

        if(other.transform.parent.GetComponent<PlayerController>())
        {
            Debug.Log("Has plauyer controller");
            if(other.transform.parent.GetComponent<PlayerController>().isPlayer1 == thrownPlayer)
            {
                willHappen = false;
            }
            else
            {
                willHappen = true;
                other.transform.parent.GetComponent<PlayerController>().PlayerDies();
            }
        }
        else
        {
            willHappen = true;
        }


        if(willHappen)
        {
            Debug.Log("Happened");
            if(other.gameObject.tag == "Player")

            if(state == STATES.IN_AIR)
            {
                state = STATES.FALLING;
            }
            else if(state == STATES.FALLING && other.gameObject.tag == "Ground")
            {
                state = STATES.ON_GROUND;
            }
        }
    }

}





