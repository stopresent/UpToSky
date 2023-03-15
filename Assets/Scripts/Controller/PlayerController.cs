using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // player�� Rigidbody2D�� Collider2D
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;

    Define.State _state = Define.State.None;
    public Define.State State { get { return _state; } set { _state = value; } }

    // player�� ��ġ�� �����´�.
    [HideInInspector] public Vector3 pos { get { return transform.position; } }
    public bool isContactAnything = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<Collider2D>();
    }

    // player�� ���������� �����̴� �Լ�
    public void Push(Vector2 force)
    {
        // AddForce: ���� ���� AddForce(���� * ���� ��, ���� ����)
        // ForceMode
        // (1) Force: �������̸� ������ �������� ���� ���̸� �ַ� �������� ���� ������ ��Ÿ��.
        // (2) Accel: �������̸� ������ �����Ѵ�.
        // (3) Impulse: �ҿ������̸� ������ �������� �ʴ´�. ª�� ������ ��, �浹�̳� ���߰� ���� �Ϳ� ���δ�.
        // (4) Velocity: �ҿ������̸� ������ �����Ѵ�.

        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Block" && collision.gameObject.tag != "Ground")
            return;

        isContactAnything = true;

        // ������ ȿ���� ���⿡�� �߰�

        // �̲������� ��
        if (collision.gameObject.name == "SlipBlock")
        {
            return;
        }

        // ���� Ƣ�� ��
        if (collision.gameObject.name == "BouncyBlock")
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 3.0f), ForceMode2D.Impulse);
            State = Define.State.BouncyState;
            return;
        }

        // ���ⱸ ���
        if (collision.gameObject.name == "AirBalloonBlock")
            StartCoroutine(ItsAirBalloon(collision.gameObject));

        // ����
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

        // ���ⱸ�� ��Ҵٰ� ��������
        if (collision.gameObject.name == "AirBalloonBlock")
            StartCoroutine(ItWasAirBalloon(collision.gameObject));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BlackHole")
            // StartCoroutine(Boom()); �̷��� �ڵ带 �ۼ��ϸ� Stop�� �ȵ�
            // �Ѵ� string���� �־���� stop�� �����ϴ�.
            StartCoroutine("Boom");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Block")
            return;

        // ��Ȧ
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
