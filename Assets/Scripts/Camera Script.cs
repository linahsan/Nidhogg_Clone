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

   private float maxX;
   private float minX;

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
           newPosition = new Vector3(updateActivePlayers[0].GetComponent<Transform>().position.x, 0, 0);
        }
        else if(activePlayerCount == 2)
        {
            newX = updateActivePlayers[0].GetComponent<Transform>().position.x;
            newX += updateActivePlayers[1].GetComponent<Transform>().position.x;
            newX *= (1/2);
            newPosition = new Vector3(newX, 0, 0);
            Debug.Log("Happened");

        }

        gameObject.GetComponent<Transform>().position = newPosition;

        if(gameObject.GetComponent<Transform>().position.x > maxX)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(maxX, 0, 0);
        }

        if (gameObject.GetComponent<Transform>().position.x < minX)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(minX, 0, 0);
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
        Debug.Log(activePlayers.Count);
    }
    
}
