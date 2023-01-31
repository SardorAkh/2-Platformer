using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector2 direction;

    [SerializeField]private float speed;

    void Update()
    {
        transform.position += (Vector3)direction * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GlobalEvent.InvokeOnDamage();
            Destroy(gameObject);
        }
    }
}
