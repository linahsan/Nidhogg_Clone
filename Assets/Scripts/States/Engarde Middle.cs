using UnityEngine;

public class ENGARDE_MIDDLE : MonoBehaviour
{
    public PlayerManger playerManager;
    void Start()
    {
        playerManager = gameObject.GetComponent<PlayerManger>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void altUpdate()
    {

        //transitions away
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerManager.stateEnd(PlayerManger.PlayerStates.ENGARDE_HIGH);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            playerManager.stateEnd(PlayerManger.PlayerStates.ENGARDE_LOW);
        }
    }

    public void altBeginState()
    {
        //change sprite?
    }
    public void altEndState()
    {
        //nothing I can think of
    }
}
