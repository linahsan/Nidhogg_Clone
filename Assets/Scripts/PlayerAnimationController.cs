using System;
using UnityEngine;



public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    public bool isDiveKick = false;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void OnAttackFinished()
    {
        animator.SetTrigger("AttackFinished");
    }

    private void Update()
    {
        isDiveKick = animator.GetCurrentAnimatorStateInfo(0).IsName("Armed_DiveKick");
    }

    public void AnimationEnded()
    {
        animator.SetTrigger("AnimationDone");
    }

    public void Dies()
    {
        playerController.PlayerDies();
        Destroy(gameObject);
    }
}
