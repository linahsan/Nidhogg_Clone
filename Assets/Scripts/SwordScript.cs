using System;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public GameObject currentPlayer;
    public bool isHeld = true;

    public enum SWORD_STATES
    {
        HELD,
        THROWN,
        FALLING,
        LAYING
    }

    public SWORD_STATES State;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isHeld)
            return; 
        other.transform.parent.GetComponent<PlayerController>().HandleHit(other as BoxCollider2D);
        
    }

    public void SetOwner(SwordOwner owner)
    {
        switch (owner)
        {
            case SwordOwner.Player1:
                State = SWORD_STATES.HELD;
                GetComponent<BoxCollider2D>().includeLayers = 1<<LayerMask.NameToLayer("Player 2 Hit Box");
                break;
            case SwordOwner.Player2:
                State = SWORD_STATES.HELD;
                GetComponent<BoxCollider2D>().includeLayers = 1<<LayerMask.NameToLayer("Player 1 Hit Box");
                break;
            case SwordOwner.None:
                State = SWORD_STATES.FALLING;
                GetComponent<BoxCollider2D>().includeLayers = 1<<LayerMask.NameToLayer("Default");
                break;
        }

        isHeld = true;
    }
    
    public enum SwordOwner {Player1, Player2, None}
}
