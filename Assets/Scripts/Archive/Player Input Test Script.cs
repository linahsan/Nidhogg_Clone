//using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class PlayerInputTestScript : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    public bool isPlayer1;
    public string response;



    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        /*
        if (isPlayer1)
        {
            playerInput.defaultControlScheme = "PLAYER3";
            Debug.Log("happened_1");
        }
        else
        {
            playerInput.defaultControlScheme = "Player2";
            Debug.Log("happened");
        }
        Debug.Log(playerInput.currentControlScheme);
        */
    }

    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.actions["Right"].triggered)
        {
            Debug.Log(response);
            Debug.Log("happened");
        }

        if(playerInput.actions["Right"].IsPressed())
        {

        }

        if(playerInput.actions["Right"].WasReleasedThisFrame())
        {
            
        }
    }
}
