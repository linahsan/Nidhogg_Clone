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

    private void Update()
    {
        
    }
}
