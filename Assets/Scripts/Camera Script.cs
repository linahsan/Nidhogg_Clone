using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEditor.SearchService;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
   public List<GameObject> activePlayers;
   private int activePlayerCount;
   private List<GameObject> updateActivePlayers = new List<GameObject>();
    private Vector3 newPosition;
   private float newX;

   [SerializeField] private float maxX;
   [SerializeField] private float minX;
   [SerializeField] private float barrierRange;

   private float frameMaxX;
   private float frameMinX;
   public int winningDirection = 0;
   
   public GameObject player1;
   public GameObject player2;
   
   public Transform player1Transform;
   public Transform player2Transform;

   public GameObject goOrange;
   public GameObject goYellow;

   public bool hasStarted = false;

   
    void Start()
    {
        goOrange = transform.GetChild(3).gameObject;
        goYellow = transform.GetChild(4).gameObject;
       //OnSceneEnter();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMaxAndMins();
        //Debug.Log(frameMinX);
        //Debug.Log(frameMaxX);
        activePlayerCount = 0;
        updateActivePlayers.Clear();
        HandleGoSigns();
        SceneTransitionManager.Instance.winningDirection = winningDirection;
        hasStarted = true;

        

        for(int i = 0; i < activePlayers.Count; i++)
        {
            if(activePlayers[i].GetComponent<PlayerController>().isAlive == true)
            {
                activePlayerCount++;
                updateActivePlayers.Add(activePlayers[i]);
            }
        }

        if(activePlayerCount == 0)
        {
            
        }
        else if(activePlayerCount == 1)
        {
           newPosition = new Vector3(updateActivePlayers[0].GetComponent<Transform>().position.x, 0, -10);
        }
        else if(activePlayerCount == 2)
        {
            newX = activePlayers[0].GetComponent<Transform>().position.x;
            newX += activePlayers[1].GetComponent<Transform>().position.x;
            newX /= 2;
            newPosition = new Vector3(newX, 0, -10);

        //handles maxes and mins:
        if(newPosition.x > frameMaxX)
        {
            newPosition = new Vector3(frameMaxX, 0, -10);
        }

        if (newPosition.x < frameMinX)
        {
            newPosition = new Vector3(frameMinX, 0, -10);
            //Debug.Log("happened");
            //Debug.Log(frameMinX);
        }
        }

        //ADD LEANING IF STATEMENT HERE
        /*
        if(Vector3.Distance(newPosition, transform.position) < 2.0f)
        {
            gameObject.GetComponent<Transform>().position = newPosition;
        }
        else
        {
            
        }
        */

        gameObject.GetComponent<Transform>().position = newPosition;

        /*
        if(gameObject.GetComponent<Transform>().position.x > frameMaxX)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(frameMaxX, 0, -10);
        }

        if (gameObject.GetComponent<Transform>().position.x < frameMinX)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(frameMinX, 0, -10);
            Debug.Log("happened");
            Debug.Log(frameMinX);
        }
        */
        


    }


    
    public void AddActivePlayer(GameObject currentPlayer)
    {
        for(int i = 0; i < activePlayers.Count; i++)
        {
            if(activePlayers[i] == currentPlayer)
            {
                return;
            }

           
        }
        activePlayers.Add(currentPlayer);
    }


    private void UpdateMaxAndMins()
    {

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        //GameObject testPlayer = null;


        if(winningDirection == 0)
        {
            
           frameMinX = gameObject.GetComponent<Transform>().position.x;
           frameMaxX = gameObject.GetComponent<Transform>().position.x;


            rightBorder.SetActive(true);
            leftBorder.SetActive(true);

            rightBorder.layer = LayerMask.NameToLayer("Wall");
            leftBorder.layer = LayerMask.NameToLayer("Wall");

        }
        else if(winningDirection == 1)
        {
            frameMinX = player1Transform.position.x - (barrierRange)*width;
            frameMaxX = player1Transform.position.x + (barrierRange)*width;

            if(player2Transform.position.x - (barrierRange)*width > frameMinX)
            {
                frameMinX = player2Transform.position.x - (barrierRange)*width;
            }

            //rightBorder.layer = LayerMask.NameToLayer("Player 1 Ignore");
            //leftBorder.layer = LayerMask.NameToLayer("Player 2 Ignore");
        }
        else if(winningDirection == -1)
        {
            frameMaxX = player2Transform.position.x + (barrierRange)*width; 

            frameMinX = player2Transform.position.x - (barrierRange)*width;

            if(player1Transform.position.x + (barrierRange)*width < frameMaxX)
            {
                frameMaxX = player1Transform.position.x + (barrierRange)*width;
            }

            //rightBorder.layer = LayerMask.NameToLayer("Player 1 Ignore");
            //leftBorder.layer = LayerMask.NameToLayer("Player 2 Ignore");
        }
    }

    public void PlayerDies(GameObject player)
    {
        if(activePlayers.Count < 2)
        {
            return;
        }
        PlayerController playerController = player.GetComponent<PlayerController>();
        GameObject testPlayer = null;
        PlayerController otherPlayerController;


        if(player.GetComponent<PlayerController>().isPlayer1) //i.e. if player 1 dies
        {

            for(int i = 0; i < activePlayers.Count; i++)
            {
                if(!activePlayers[i].GetComponent<PlayerController>().isPlayer1)
                {
                    testPlayer = activePlayers[i];
                }
            }

            otherPlayerController = testPlayer.GetComponent<PlayerController>();


            if(otherPlayerController.IsAlive())
            {
                winningDirection = -1;
            }
            else
            {
                winningDirection = 0;
            }

        }
        else //i.e. if player 2 died
        {
            for(int i = 0; i < activePlayers.Count; i++)
            {
                if(activePlayers[i].GetComponent<PlayerController>().isPlayer1)
                {
                    testPlayer = activePlayers[i];
                }
            }

            otherPlayerController = testPlayer.GetComponent<PlayerController>();


            if(otherPlayerController.IsAlive())
            {
                winningDirection = 1;
            }
            else
            {
                winningDirection = 0;
            }
        }

        SceneTransitionManager.Instance.winningDirection = winningDirection;

        //Debug.Log(player);
        //Debug.Log(testPlayer);
        //Debug.Log(otherPlayerController.IsAlive());
    }

    public void Initalization()
    {
        if (activePlayers.Count > 1)
        {
            for (int i = 0; i < activePlayers.Count; i++)
            {
                if (activePlayers[i].GetComponent<PlayerController>().isPlayer1)
                {
                    player1 = activePlayers[i];
                }
                else
                {
                    player2 = activePlayers[i];
                }
            }
            player1Transform = player1.GetComponent<Transform>();
            player2Transform = player2.GetComponent<Transform>();
            player1.GetComponent<PlayerController>().GetOtherPlayerVariables();
            player2.GetComponent<PlayerController>().GetOtherPlayerVariables();


            //JUST MAKE A SERIES OF IF STATEMENTS THAT DO THESE IN A SPECIFIC ORDER DEPENDING ON THE WINNINGDIRECTION

            //player1.GetComponent<PlayerController>().OnEnterScene();
            //player2.GetComponent<PlayerController>().OnEnterScene();
        }
        
    }

    public void HandleGoSigns()
    {
        if(winningDirection == 1)
        {
            goYellow.SetActive(true);
            goOrange.SetActive(false);
        }
        else if(winningDirection == -1)
        {
            goYellow.SetActive(false);
            goOrange.SetActive(true);
        }
        else
        {
            goYellow.SetActive(false);
            goOrange.SetActive(false);
        }
    }

    public void OnSceneEnter()
    {
        Debug.Log("Camera Script");
        winningDirection = SceneTransitionManager.Instance.winningDirection;
        transform.position = new Vector3(SceneTransitionManager.Instance.cameraStartingX, transform.position.y, transform.position.z);
        Debug.Log(winningDirection);
        //hasStarted = true;
    }
    
}

