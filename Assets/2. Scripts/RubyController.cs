using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public const float FIXED_PT = 0.5f;
    public const int FORCE_PT = 300;
    // 이동 속도 상수값 지정
    public float moveSpeed = 4.0f;

    public int maxHealth = 5;
    public int health { get { return currentHealth;}}
    public float timeInvincible = 2.0f;
    public GameObject projectilePrefab;
    public ParticleSystem HitEffectPrefab;
    public AudioClip throwSound;
    public AudioClip hitSound;

    
    private bool isInvincible;
    private float invincibleTimer;
    private int currentHealth;
    private Vector2 position;
    private Animator animator;
    private AudioSource audioSource;
    
    private Vector2 lookDirection = new Vector2(1,0);

    // 캐릭터의 지속 충돌 시 떨림 현상 방지를 위한 조치
    private Rigidbody2D rb2d;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // 캐릭터에 존재하는 Rigidbody2D를 달라고 요구
        currentHealth = maxHealth;
        position = rb2d.position;
        animator = GetComponent<Animator>();

        audioSource= GetComponent<AudioSource>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();

                if (character != null)
                {
                    character.DisplayDialog();
                }  
            }
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

            PlaySound(hitSound);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0 , maxHealth);
        
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    private void Launch()
    {
        GameObject projectileObject = Instantiate(
            projectilePrefab, 
            rb2d.position + Vector2.up * FIXED_PT,
            Quaternion.identity);
        Projectile projectile = 
            projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, FORCE_PT);

        animator.SetTrigger("Launch");

        PlaySound(throwSound);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}
