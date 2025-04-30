using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerInputScript input; 
    [SerializeField] Animator animator;
    
    // Standing Idle Heights
    private int currentHeight = 1;
    
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        HeightCheck();
    }

    void HeightCheck()
    {
        if (input.DownPressed() && animator.GetInteger("height") > 0)
        {
            currentHeight--;
            UpdateHeightAnimation();
        }
        else if (input.UpPressed() && animator.GetInteger("height") < 2)
        {
            currentHeight++;
            UpdateHeightAnimation();
        }
    }

    void RunCheck() // TO BE REFINED
    {
        if (input.LeftPressed())
        {
            UpdateRunAnimation();
        }
        else if (input.RightPressed())
        {
            UpdateRunAnimation();
        }
    }

    void UpdateHeightAnimation()
    {
        animator.SetInteger("height", currentHeight);
    }

    void UpdateRunAnimation() // TO BE REFINED
    {
        animator.SetFloat("speed", playerController.moveSpeed);
    }
}
