using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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
   
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateMaxAndMins();
        //Debug.Log(frameMinX);
        //Debug.Log(frameMaxX);
        activePlayerCount = 0;
        updateActivePlayers.Clear();

        

        for(int i = 0; i < activePlayers.Count; i++)
        {
            if(activePlayers[i].GetComponent<SpriteRenderer>().enabled == true)
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


        }
        gameObject.GetComponent<Transform>().position = newPosition;

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

            /*

            frameMaxX = maxX;
            frameMinX = minX;
            //set min
            for(int i = 0; i < activePlayers.Count; i++)
            {
                if(activePlayers[i].GetComponent<Transform>().position.x - (barrierRange)*width > frameMinX)
                {
                    frameMinX = activePlayers[i].GetComponent<Transform>().position.x - (barrierRange)*width;
                }
            }

            //set max

            for(int i = 0; i < activePlayers.Count; i++)
            {
                if(activePlayers[i].GetComponent<Transform>().position.x - (barrierRange)*width < frameMaxX)
                {
                    frameMaxX = activePlayers[i].GetComponent<Transform>().position.x + (barrierRange)*width;
                }
            }
            */
            //Debug.Log("0");
        }
        else if(winningDirection == 1)
        {
            /*
            for(int i = 0; i < activePlayers.Count; i++)
            {
                if(!activePlayers[i].GetComponent<PlayerController>().isPlayer1)
                {
                    testPlayer = activePlayers[i];
                }
            }
            */
            

            frameMinX = player1Transform.position.x - (barrierRange)*width;
            //frameMaxX = testPlayer.GetComponent<Transform>().position.x + (barrierRange)*width; 
            frameMaxX = player1Transform.position.x + (barrierRange)*width;

            if(player2Transform.position.x - (barrierRange)*width > frameMinX)
            {
                frameMinX = player2Transform.position.x - (barrierRange)*width;
            }

            rightBorder.SetActive(false);
            leftBorder.SetActive(false);
            //Debug.Log("1");
        }
        else if(winningDirection == -1)
        {
            /*
            for(int i = 0; i < activePlayers.Count; i++)
            {
                if(activePlayers[i].GetComponent<PlayerController>().isPlayer1)
                {
                    testPlayer = activePlayers[i];
                }
            }
            */

            //frameMinX = testPlayer.GetComponent<Transform>().position.x - (barrierRange)*width;
            frameMaxX = player2Transform.position.x + (barrierRange)*width; 

            frameMinX = player2Transform.position.x - (barrierRange)*width;

            if(player2Transform.position.x + (barrierRange)*width < frameMaxX)
            {
                frameMaxX = player2Transform.position.x + (barrierRange)*width;
            }
            rightBorder.SetActive(false);
            leftBorder.SetActive(false);
            //Debug.Log("-1");
        }
    }

    public void PlayerDies(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        GameObject testPlayer = null;
        PlayerController otherPlayerController;


        if(player.GetComponent<PlayerController>().isPlayer1) //i.e. if player 1 dies
        {
            //get player 2

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

            //gets player 1
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

        Debug.Log(player);
        Debug.Log(testPlayer);
        Debug.Log(otherPlayerController.IsAlive());
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
        }
        
    }
    
}

