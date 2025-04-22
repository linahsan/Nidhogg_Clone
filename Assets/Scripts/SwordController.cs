using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] bool isHeld = true;
    [SerializeField] int swordHeight;
    [SerializeField] Transform playerPos;
    
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        FollowPlayer();
    }
    /* ignore this code for now, just to remind us how we're doing the sword when its active
      - either we can make the sword part of the sprites when inactive (running, jumping,etc.), or do 
      what the nidhogg ppl did,  which i think, based on our imported sprites, was have the sword object 
      following the players hand
    */ 
    void FollowPlayer()
    {
        if (isHeld)
        {
           transform.position = playerPos.position;
        }
    }
}
