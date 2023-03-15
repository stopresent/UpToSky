using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // player의 Rigidbody2D와 Collider2D
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;

    Define.State _state = Define.State.None;
    public Define.State State { get { return _state; } set { _state = value; } }

    // player의 위치를 가져온다.
    [HideInInspector] public Vector3 pos { get { return transform.position; } }
    public bool isContactAnything = false;

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
        if (collision.gameObject.tag != "Block" && collision.gameObject.tag != "Ground")
            return;

        isContactAnything = true;

        // 블럭들의 효과들 여기에서 추가

        // 미끄러지는 블럭
        if (collision.gameObject.name == "SlipBlock")
        {
            return;
        }

        // 통통 튀는 블럭
        if (collision.gameObject.name == "BouncyBlock")
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 3.0f), ForceMode2D.Impulse);
            State = Define.State.BouncyState;
            return;
        }

        // 열기구 블록
        if (collision.gameObject.name == "AirBalloonBlock")
            StartCoroutine(ItsAirBalloon(collision.gameObject));

        // 착지
        if (GetComponent<Rigidbody2D>().velocity.y < 0 && collision.gameObject.tag != "Ground")
        {
            Managers.Sound.Play("Sound_Landing");
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        else if (GetComponent<Rigidbody2D>().velocity.y >= 0 && collision.gameObject.tag != "Ground")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -GetComponent<Rigidbody2D>().velocity.y);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Block" && collision.gameObject.tag != "Ground")
            return;

        isContactAnything = false;

        // 열기구랑 닿았다가 떨어지면
        if (collision.gameObject.name == "AirBalloonBlock")
            StartCoroutine(ItWasAirBalloon(collision.gameObject));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BlackHole")
            // StartCoroutine(Boom()); 이렇게 코드를 작성하면 Stop이 안됨
            // 둘다 string으로 넣어줘야 stop이 가능하다.
            StartCoroutine("Boom");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Block")
            return;

        // 블랙홀
        if (collision.gameObject.name == "BlackHole")
        {
            Vector3 dir = collision.gameObject.transform.position - gameObject.transform.position;
            Vector3.Normalize(dir);
            rb.AddForce(dir * 70, ForceMode2D.Force);
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BlackHole")
            StopCoroutine("Boom");
    }

    IEnumerator ItsAirBalloon(GameObject it)
    {
        yield return new WaitForSeconds(1.0f);
        it.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        it.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
    }

    IEnumerator ItWasAirBalloon(GameObject it)
    {
        it.GetComponent<Rigidbody2D>().gravityScale = -0.2f;
        Destroy(it.GetComponent<CapsuleCollider2D>());
        yield return new WaitForSeconds(4.0f);
        Destroy(it);
    }

    IEnumerator Boom()
    {
        yield return new WaitForSeconds(3.0f);
        Managers.UI.ShowPopupUI<UI_Dead>();
        Destroy(GameObject.Find("Player").GetComponent<PlayerController>());
    }

}
