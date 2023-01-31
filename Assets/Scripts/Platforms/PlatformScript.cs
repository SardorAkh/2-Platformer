using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("ActivatePlatform");
        }
    }
}
