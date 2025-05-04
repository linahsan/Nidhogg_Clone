using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public BoxCollider2D thisCollider;
    public CameraScript cameraScript;

    public bool rightDoor;

    public string destinationScene;
    public int destinationStartingPlayerX;
    public int destinationStartingPlayerY;
    public int destinationStartingCameraX;
    void Start()
    {
        thisCollider = gameObject.GetComponent<BoxCollider2D>();
        cameraScript = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraScript>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.gameObject.GetComponent<PlayerController>().isPlayer1 && cameraScript.winningDirection == 1)
            {
                SceneTransitionManager.Instance.SceneTransition(destinationScene, destinationStartingPlayerX, destinationStartingPlayerY, destinationStartingCameraX);
            }
            else if(!collision.gameObject.GetComponent<PlayerController>().isPlayer1 && cameraScript.winningDirection == -1)
            {
                SceneTransitionManager.Instance.SceneTransition(destinationScene, destinationStartingPlayerX, destinationStartingPlayerY, destinationStartingCameraX);
            }
        }
    }
}
