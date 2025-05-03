using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
   public List<GameObject> activePlayers;
   private int activePlayerCount;
   private List<GameObject> updateActivePlayers = new List<GameObject>();
    private Vector3 newPosition;
   private float newX;

   [SerializeField] private float maxX;
   [SerializeField] private float minX;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log(newX);
            newX += activePlayers[1].GetComponent<Transform>().position.x;
            Debug.Log(newX);
            newX /= 2;
            Debug.Log(newX);
            newPosition = new Vector3(newX, 0, -10);
            /*
            Debug.Log(activePlayers.Count);
            Debug.Log(activePlayers[0]);
            Debug.Log(activePlayers[0].GetComponent<Transform>().position.x);
            Debug.Log("Happened");
            Debug.Log(newX);
            */

        }
        Debug.Log(newPosition);
        gameObject.GetComponent<Transform>().position = newPosition;
        Debug.Log(gameObject.GetComponent<Transform>().position.x);

        /*
        if(gameObject.GetComponent<Transform>().position.x > maxX)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(maxX, 0, -10);
        }

        if (gameObject.GetComponent<Transform>().position.x < minX)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(minX, 0, -10);
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
        Debug.Log(activePlayers.Count);
    }
    
}
