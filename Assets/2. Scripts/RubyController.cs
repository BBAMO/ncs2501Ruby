using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // 이동 속도 상수값 지정
    const float xs = 4.0f;
    const float ys = 4.0f;
    void Start()
    {
        
    }


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // GetAxisRaw를 사용하면 -1, 1 값이 넘어온다.
        //float vertical = Input.GetAxisRaw("Vertical");
        //Debug.Log($"H:{horizontal}");
        //Debug.Log($"V: {vertical}");
        
        Vector2 position = transform.position;
        position.x += xs * horizontal * Time.deltaTime;
        position.y += ys * vertical * Time.deltaTime;
        transform.position = position;
    }
}
