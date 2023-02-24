using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // player�� Rigidbody2D�� Collider2D
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;

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
            rb.AddForce(new Vector2(1.0f, 0.0f), ForceMode2D.Impulse);
            return;
        }

        // ���� Ƣ�� ��
        if (collision.gameObject.name == "BouncyBlock")
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 1.0f), ForceMode2D.Impulse);
            return;
        }

        // �������� ���
        if (collision.gameObject.name == "BreakableBlock")
            StartCoroutine(MakeItFall(collision.gameObject));

        // ���ⱸ ���
        if (collision.gameObject.name == "AirBalloonBlock")
            StartCoroutine(ItsAirBalloon(collision.gameObject));

        // ����
        if (GetComponent<Rigidbody2D>().velocity.y <= 5)
    {
        Managers.Sound.Play("Sound_Landing");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
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

    IEnumerator MakeItFall(GameObject it)
    {
        yield return new WaitForSeconds(3.0f);
        if (it == null)
            yield break;
        it.GetComponent<CapsuleCollider2D>().isTrigger = true;
        it.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        it.GetComponent<Rigidbody2D>().gravityScale = 1;

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

}
