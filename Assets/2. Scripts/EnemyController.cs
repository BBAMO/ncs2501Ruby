using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    public int needFix = 3;

    private Rigidbody2D rb2d;
    private float timer;
    private int direction = 1;
    private Vector2 position;
    private Animator animator;
    private bool broken;
    private int fixedCount;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        position = rb2d.position;
        animator = GetComponent<Animator>();
        broken = true;
        fixedCount = 0;
    }

    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        
        if (vertical)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
            position.y += moveSpeed * direction * Time.deltaTime;
        }
        else
        {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
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

    public void Fix()
    {
        if (fixedCount >= needFix)
        {
        broken = false;
        rb2d.simulated = false;
        animator.SetTrigger("Fixed");
        }
        else
        {
            fixedCount++;
        }
    }
}
