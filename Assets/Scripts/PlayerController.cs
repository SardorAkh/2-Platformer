using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;

    [SerializeField] private float maxSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float maxJumpForce;

    private Rigidbody2D rb;

    [SerializeField] private bool isGrounded;

    [SerializeField] private Transform respawnPoint;

    [SerializeField] private AudioClip jumpAudioClip;
    [SerializeField] private AudioClip hitAudioClip;
    [SerializeField] private AudioClip coinCollectAudioClip;
    [SerializeField] private AudioClip fallOnGroundAudioClip;

    private AudioSource audioSource;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    [SerializeField] PlayerBullet bulletPrefab;
    
    [SerializeField] float _shootRate;
     float _shootTime;

    [SerializeField] int maxAmmo;
    [SerializeField] int ammo;
    [SerializeField] int ammoCount;
    [SerializeField] float reloadTime;
    [SerializeField] float _reload;
    [SerializeField] TextMeshProUGUI ammoText;
    void SetEventListeners()
    {
        GlobalEvent.OnResetPlayerPosition += ResetPosition;
        GlobalEvent.OnDamage += OnDamage;
        GlobalEvent.OnReachCheckpoint += OnReachCheckpoint; 
    }
    void RemoveAllEventListeners()
    {
        GlobalEvent.OnResetPlayerPosition -= ResetPosition;
        GlobalEvent.OnDamage -= OnDamage;
        GlobalEvent.OnReachCheckpoint -= OnReachCheckpoint;
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        SetEventListeners();
        ammoText.text = $"{ammo}/{ammoCount}";

    }

    void Update()
    {
        var dt = Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;
            anim.SetBool("Running", true);  
            rb.AddForce((Vector2.left * speed * dt));
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetBool("Running", false);

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false;
            anim.SetBool("Running", true);

            rb.AddForce((Vector2.right * speed * dt));
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetBool("Running", false);

        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audioSource.PlayOneShot(jumpAudioClip);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            if(ammoCount != 0 && _shootTime > _shootRate && ammo != 0 )
            {
                _shootTime = 0;
                PlayerBullet bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                bullet.direction = !spriteRenderer.flipX ? Vector2.right : Vector2.left;
                bullet.DistanceToDestroy = 10;
                bullet.origin = transform.position;
                ammo--;

                ammoText.text = $"{ammo}/{ammoCount}";
            }
        }
        _shootTime += Time.deltaTime;
        if (ammo == 0)
        {
            _reload += Time.deltaTime;
            if(_reload >= reloadTime)
            {
                ammo = maxAmmo;
                _reload = 0;
                ammoCount -= ammo;
                ammoText.text = $"{ammo}/{ammoCount}";
            }
        }

        Vector2 speedVelocity = new Vector2(rb.velocity.x, 0);
        Vector2 jumpVelocity = new Vector2(0, rb.velocity.y);

        speedVelocity = Vector2.ClampMagnitude(speedVelocity, maxSpeed);
        jumpVelocity = Vector2.ClampMagnitude(jumpVelocity, maxJumpForce);
        rb.velocity = speedVelocity + jumpVelocity;
        anim.SetFloat("SpeedY", rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(coinCollectAudioClip);
            GlobalEvent.InvokeOnCoinCollect();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("IsGrounded",isGrounded);
        }
        if(collision.gameObject.CompareTag("Ammo"))
        {
            ammoCount += maxAmmo;
            ammoText.text = $"{ammo}/{ammoCount}";
            Destroy(collision.gameObject);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            anim.SetBool("IsGrounded",isGrounded);
        }
    }

    private void ResetPosition()
    {
        transform.position = respawnPoint.transform.position;
    }

    private void OnDamage()
    {   
        audioSource.PlayOneShot(hitAudioClip);
    }
    private void OnReachCheckpoint(CheckpointTrigger c)
    {
        respawnPoint.position = c.gameObject.transform.position;
    }
    private void OnDestroy()
    {
        RemoveAllEventListeners();
    }
}
