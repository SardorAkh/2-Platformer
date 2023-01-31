using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Move : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 500;
    private Rigidbody2D rb;
    [SerializeField] private int Coin;
    public static Action<int> OnCollectionCoin;
    [SerializeField]
    private bool IsGround;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left*speed*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space)&&IsGround)
        {
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Coin++;
            OnCollectionCoin?.Invoke(Coin);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGround = false;
        }
    }
}
