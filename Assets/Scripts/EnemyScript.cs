using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] float _rayLength; 

    private Vector3 prevPos;
    private Vector2 _rayDirection;

    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        
        if (collision.gameObject.CompareTag("Player"))
        {
            GlobalEvent.InvokeOnDamage();
        }
    }
    private void Update()
    {

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, _rayLength, mask);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, _rayLength, mask);
        if (hitLeft || hitRight)
        {
            Vector3 target = hitLeft ? hitLeft.collider.gameObject.transform.position : hitRight.collider.gameObject.transform.position;
           
            transform.position = Vector2.MoveTowards(transform.position, target, 2 * Time.deltaTime);
        }

        prevPos = transform.position;
    }
}
