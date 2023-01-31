using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeamboScript : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    [SerializeField]
    private List<GameObject> bullets;

    private Vector2 shootDir;

    
    void OnTriggerEnter2D(Collider2D collision)
    {   
        Debug.Log(collision.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;

            shootDir = Vector2.zero;
            if (transform.position.x <= player.transform.position.x)
            {
                shootDir = Vector2.right;
            }
            else
            {
                shootDir = Vector2.left;
            }

            InvokeRepeating("Shoot", 0f, 2f);

        }
    }
    void Shoot()
    {
        var bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>().direction = shootDir;
        bullets.Add(bullet);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            CancelInvoke("Shoot");
        }
    }
}
