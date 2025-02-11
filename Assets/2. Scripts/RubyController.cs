using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // 이동 속도 상수값 지정
    public float moveSpeed = 4.0f;

    public int maxHealth = 5;
    public int health { get { return currentHealth;}}
    public float timeInvincible = 2.0f;

    
    private bool isInvincible;
    private float invincibleTimer;
    private int currentHealth;
    private Vector2 position;
    private Animator animator;
    private Vector2 lookDirection = new Vector2(1,0);

    // 캐릭터의 지속 충돌 시 떨림 현상 방지를 위한 조치
    private Rigidbody2D rb2d;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // 캐릭터에 존재하는 Rigidbody2D를 달라고 요구
        currentHealth = maxHealth;
        position = rb2d.position;
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // GetAxisRaw를 사용하면 -1, 1 값이 넘어온다.
        //float vertical = Input.GetAxisRaw("Vertical");
        //Debug.Log($"H:{horizontal}");
        //Debug.Log($"V: {vertical}");
        
        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || 
            !Mathf.Approximately(move.y, 0.0f))
            {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
            }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        //Vector2 position = rb2d.position;
        //position.x += moveSpeed * horizontal * Time.deltaTime;
        //position.y += moveSpeed * vertical * Time.deltaTime;
        position += move * moveSpeed * Time.deltaTime;
        //transform.position = position; - 캐릭터의 지속 충돌 시 떨림 현상 방지를 위한 조치
        rb2d.MovePosition(position);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0 , maxHealth);
        Debug.Log($"{currentHealth} / {maxHealth}");
    }
}
