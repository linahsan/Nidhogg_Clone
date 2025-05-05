using UnityEngine;

public class WinningScript : MonoBehaviour
{
    private bool hasWon = false;
    public GameObject textObject;
    public Transform cameraTransform;

    void Start()
    {
        cameraTransform = GameObject.FindWithTag("Camera").GetComponent<Transform>();
        textObject = transform.GetChild(0).gameObject;
        //Debug.Log(textObject);
        textObject.SetActive(false);
    }

    void Update()
    {
        if(hasWon)
        {
            textObject.SetActive(true);
            transform.position = cameraTransform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            hasWon = true;
            collision.gameObject.SetActive(false);
        }
    }
}
