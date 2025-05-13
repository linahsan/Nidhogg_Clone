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
    

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATES.IN_AIR:
                gameObject.transform.Translate(new Vector3 (directionValue * speed, 0, 0));
                break;

            case STATES.FALLING:
                gameObject.transform.Translate(new Vector3 (0, -speed, 0));
                break;
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Happened");

        bool willHappen = false;

        if(other.transform.parent.GetComponent<PlayerController>())
        {
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





