using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public GameObject currentPlayer;

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
}
