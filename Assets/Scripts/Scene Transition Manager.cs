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
