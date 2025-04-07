using UnityEngine;

public class PlayerManger : MonoBehaviour
{
    public enum PlayerStates
    {
        ENGARDE_HIGH,
        ENGARDE_MIDDLE,
        ENGARDE_LOW,
        LUNGE_HIGH,
        LUNGE_MIDDLE,
        LUNGE_LOW,
        RUNNING,
        JUMPING,
        FALLING,
        CROUCHING,
        DYING
    }

    private PlayerStates State;

    public ENGARDE_HIGH engardeHighScript;
    public ENGARDE_MIDDLE engardeMiddleScript;
    public ENGARDE_LOW engardeLowScript;
    
    void Start()
    {
        //get the state scripts:
        engardeHighScript = gameObject.GetComponent<ENGARDE_HIGH>();
        engardeMiddleScript = gameObject.GetComponent<ENGARDE_MIDDLE>();
        engardeLowScript = gameObject.GetComponent<ENGARDE_LOW>();
    }

    void Update()
    {
        stateUpdate(State);


        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(State);
        }
    }


    public void stateUpdate(PlayerStates updateState)
    {
        switch (updateState)
        {
            case PlayerStates.ENGARDE_HIGH:
                engardeHighScript.altUpdate();
                break;

            case PlayerStates.ENGARDE_MIDDLE:
                engardeMiddleScript.altUpdate();
                break;

            case PlayerStates.ENGARDE_LOW:
                engardeLowScript.altUpdate();
                break;

            
        }
    }
    
    public void stateEnd(PlayerStates newState)
    {
        switch (State)
        {
            case PlayerStates.ENGARDE_HIGH:
                engardeHighScript.altEndState();
                break;
            
            case PlayerStates.ENGARDE_MIDDLE:
                engardeMiddleScript.altEndState();
                break;

            case PlayerStates.ENGARDE_LOW:
                engardeLowScript.altEndState();
                break;

            
        }

        stateBegin(newState);
    }

    public void stateBegin(PlayerStates newState)
    {
        State = newState;

        switch (newState)
        {
            case PlayerStates.ENGARDE_HIGH:
                engardeHighScript.altBeginState();
                break;
            
            case PlayerStates.ENGARDE_MIDDLE:
                engardeMiddleScript.altBeginState();
                break;

            case PlayerStates.ENGARDE_LOW:
                engardeLowScript.altBeginState();
                break;

            
        }
    }
}
