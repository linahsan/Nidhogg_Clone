using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void OnAttackFinished()
    {
        animator.SetTrigger("AttackFinished");
    }

    public void OnHitDone()
    {
        animator.SetTrigger("AnimationDone");
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            animator.SetTrigger("AnimationDone");
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
