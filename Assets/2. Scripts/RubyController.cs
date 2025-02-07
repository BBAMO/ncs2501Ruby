using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // 이동 속도 상수값 지정
    public float moveSpeed = 50.0f;

    public int maxHealth = 5;
    public int health { get { return currentHealth;}}
    private int currentHealth;

    // 캐릭터의 지속 충돌 시 떨림 현상 방지를 위한 조치
    private Rigidbody2D rb2d;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // 캐릭터에 존재하는 Rigidbody2D를 달라고 요구
        currentHealth = 1; //maxHealth;
    }


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // GetAxisRaw를 사용하면 -1, 1 값이 넘어온다.
        //float vertical = Input.GetAxisRaw("Vertical");
        //Debug.Log($"H:{horizontal}");
        //Debug.Log($"V: {vertical}");
        
        Vector2 position = rb2d.position;
        position.x += moveSpeed * horizontal * Time.deltaTime;
        position.y += moveSpeed * vertical * Time.deltaTime;
        //transform.position = position; - 캐릭터의 지속 충돌 시 떨림 현상 방지를 위한 조치
        rb2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0 , maxHealth);
        Debug.Log($"{currentHealth} / {maxHealth}");
    }
}
