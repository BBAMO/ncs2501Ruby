using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    private Rigidbody2D rb2d;
    private float timer;
    private int direction = 1;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rb2d.position;
        
        if (vertical)
        {
            position.y += moveSpeed * direction * Time.deltaTime;
        }
        else
        {
            position.x += moveSpeed * direction * Time.deltaTime;
        }
    
        rb2d.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player =
            other.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
