using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public const float DISTANCE_DESTROY = 100.0f;
    Rigidbody2D rb2d;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.magnitude > DISTANCE_DESTROY)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rb2d.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Projectile Collision with" + other.gameObject);

        //EnemyController e = 
        //    other.collider.GetComponent<EnemyController>();
        //if (e != null)
        if (other.collider.TryGetComponent<EnemyController>(out var e))
        {
            e.Fix();
        }

        Destroy(gameObject);
    }
}
