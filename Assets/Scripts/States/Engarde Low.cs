using UnityEngine;

public class ENGARDE_LOW : MonoBehaviour
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
            playerManager.stateEnd(PlayerManger.PlayerStates.ENGARDE_MIDDLE);
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
