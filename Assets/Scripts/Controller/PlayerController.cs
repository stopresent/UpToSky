using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // player�� Rigidbody2D�� Collider2D
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;

    // player�� ��ġ�� �����´�.
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
        if (collision.gameObject.tag != "Block")
            return;
        
        if(rb.velocity.y < 0 )
            rb.velocity.Set(0, 0);
    }
}
