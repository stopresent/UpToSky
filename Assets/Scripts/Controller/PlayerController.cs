using System;
using System.Collections;
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

        // 블럭들의 효과들 여기에서 추가

        // 미끄러지는 블럭
        if (collision.gameObject.name == "SlipBlock")
        {
            rb.AddForce(new Vector2(1.0f, 0.0f), ForceMode2D.Impulse);
            return;
        }


        // 통통 튀는 블럭
        if (collision.gameObject.name == "BouncyBlock")
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 1.0f), ForceMode2D.Impulse);
            return;
        }

        if (GetComponent<Rigidbody2D>().velocity.y <= 5)
        {
            Managers.Sound.Play("Sound_Landing");
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
            
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Block")
            return;

        if(collision.gameObject.name == "BreakableBlock")
        {
            StartCoroutine(MakeItFall(collision.gameObject));
        }

    }

    IEnumerator MakeItFall(GameObject it)
    {
        yield return new WaitForSeconds(3.0f);
        if (it == null)
            yield break;
        it.GetComponent<CapsuleCollider2D>().isTrigger = true;
        it.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        it.GetComponent<Rigidbody2D>().gravityScale = 1;

    }

}
