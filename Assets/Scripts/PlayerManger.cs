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

    public ENGARDE_HIGH ENGARDE_HIGH_SCRIPT;
    public ENGARDE_MIDDLE ENGARDE_MIDDLE_SCRIPT;
    public ENGARDE_LOW ENGARDE_LOW_SCRIPT;

    void Start()
    {
        //get the state scripts:
        ENGARDE_HIGH_SCRIPT = gameObject.GetComponent<ENGARDE_HIGH>();
        ENGARDE_MIDDLE_SCRIPT = gameObject.GetComponent<ENGARDE_MIDDLE>();
        ENGARDE_LOW_SCRIPT = gameObject.GetComponent<ENGARDE_LOW>();
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
                ENGARDE_HIGH_SCRIPT.altUpdate();
                break;

            case PlayerStates.ENGARDE_MIDDLE:
                ENGARDE_MIDDLE_SCRIPT.altUpdate();
                break;

            case PlayerStates.ENGARDE_LOW:
                ENGARDE_LOW_SCRIPT.altUpdate();
                break;

            
        }
    }
    
    public void stateEnd(PlayerStates newState)
    {
        switch (State)
        {
            case PlayerStates.ENGARDE_HIGH:
                ENGARDE_HIGH_SCRIPT.altEndState();
                break;
            
            case PlayerStates.ENGARDE_MIDDLE:
                ENGARDE_MIDDLE_SCRIPT.altEndState();
                break;

            case PlayerStates.ENGARDE_LOW:
                ENGARDE_LOW_SCRIPT.altEndState();
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
                ENGARDE_HIGH_SCRIPT.altBeginState();
                break;
            
            case PlayerStates.ENGARDE_MIDDLE:
                ENGARDE_MIDDLE_SCRIPT.altBeginState();
                break;

            case PlayerStates.ENGARDE_LOW:
                ENGARDE_LOW_SCRIPT.altBeginState();
                break;

            
        }
    }
}
