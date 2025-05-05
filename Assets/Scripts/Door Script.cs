using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public BoxCollider2D thisCollider;
    public CameraScript cameraScript;

    public bool rightDoor;

    public string destinationScene;
    public float destinationStartingPlayerX;
    public float destinationStartingPlayerY;
    public float destinationStartingCameraX;
    void Start()
    {
        thisCollider = gameObject.GetComponent<BoxCollider2D>();
        cameraScript = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraScript>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            DoorSceneChange();
            Debug.Log("Happened");
        }
    }

    public void DoorSceneChange()
    {
        Debug.Log(cameraScript.winningDirection);
        Debug.Log(SceneTransitionManager.Instance.winningDirection);
        SceneTransitionManager.Instance.SceneTransition(destinationScene, destinationStartingPlayerX, destinationStartingPlayerY, destinationStartingCameraX);
    }

    /*
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
    */
}
