using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Vector2 origin;
    public float DistanceToDestroy;
    public Vector2 direction;

    [SerializeField] private float speed;

    void Update()
    {
        if (Vector2.Distance(origin, transform.position) >= DistanceToDestroy)
        {
            Destroy(gameObject);
        }

        transform.position += (Vector3)direction * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
