using UnityEngine;

public class SwordExternalScript : MonoBehaviour
{
    public enum STATES
    {
        IN_AIR,
        FALLING,
        ON_GROUND
    }
    public STATES state;
    public int directionValue;
    public float speed;
    

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATES.IN_AIR:
                break;
        }
    }
}
