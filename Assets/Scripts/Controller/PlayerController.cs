using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // player의 Rigidbody2D와 Collider2D
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;

    // player의 위치를 가져온다.
    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<Collider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "BreakableBlock")
            Destroy(collision.gameObject, 3f);
    }

    // player를 실질적으로 움직이는 함수
    public void Push(Vector2 force)
    {
        // AddForce: 쉽게 말해 AddForce(방향 * 힘의 값, 힘의 종류)
        // ForceMode
        // (1) Force: 연속적이며 질량을 무시하지 않을 때이며 주로 현실적인 물리 현상을 나타냄.
        // (2) Accel: 연속적이며 질량을 무시한다.
        // (3) Impulse: 불연속적이며 질량을 무시하지 않는다. 짧은 순간의 힘, 충돌이나 폭발과 같은 것에 쓰인다.
        // (4) Velocity: 불연속적이며 질량을 무시한다.

        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Block")
            return;
        
        if(rb.velocity.y < 0 )
            rb.velocity.Set(0, 0);
    }
}
