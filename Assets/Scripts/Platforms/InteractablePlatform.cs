using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlatform : MonoBehaviour
{   
    private Animator _animator;
    private MeshRenderer _textMesh;
    private bool _isOnLift = false;
    void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _textMesh = GetComponentInChildren<MeshRenderer>();
        _textMesh.gameObject.SetActive(false);
    }

    private void Update()
    {   if(_isOnLift)
        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetBool("ActivateLift", !_animator.GetBool("ActivateLift"));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
            _textMesh.gameObject.SetActive(true);
            _isOnLift = true;

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            

        }
            
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
            _textMesh.gameObject.SetActive(false);

            _isOnLift = false;

        }
    }
}
