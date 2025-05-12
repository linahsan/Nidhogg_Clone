using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public int winningDirection;
    public static SceneTransitionManager Instance;

    //player coords:
    public float playerSpawnX;
    public float playerSpawnY;
    public float cameraStartingX;

   private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        
        var playerList = Object.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
        var currentPlayer = playerList[0];
        var currentPlayerTransform = currentPlayer.GetComponent<Transform>();
        var currentPlayerController = currentPlayer.GetComponent<PlayerController>();
        if(currentPlayerController.isPlayer1)
        {
            if(winningDirection == -1)
            {
                currentPlayerController.isAlive = false;
                currentPlayerController.isCrouching = false;
                currentPlayerController.isFalling = false;
            }
            else if(winningDirection == 1)
            {
                currentPlayerTransform.position = new Vector3(playerSpawnX, playerSpawnY, 0);
            }
        }
        else //i.e. if player 2:
        {
            if(winningDirection == -1)
            {
                currentPlayerTransform.position = new Vector3(playerSpawnX, playerSpawnY, 0);
            }
            else if(winningDirection == 1)
            {
                currentPlayerController.isAlive = false;
                currentPlayerController.isCrouching = false;
                currentPlayerController.isFalling = false;
            }
        }

        GameObject.FindWithTag("Camera").GetComponent<CameraScript>().OnSceneEnter();
        
    }

    
    void Update()
    {
        
    }

    public void SceneTransition(string sceneName, float winningPlayerX, float winningPlayerY, float newCameraX)
    {
        playerSpawnX = winningPlayerX;
        playerSpawnY = winningPlayerY;
        cameraStartingX = newCameraX;



        SceneManager.LoadScene(sceneName);
    }

}
