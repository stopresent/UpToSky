using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    Vector3 playerVector;
    Rigidbody2D _rb;
    LineRenderer _lr;
    public Collider2D _col;
    Define.State _state = Define.State.None;
    public Define.State State { get { return _state; } set { _state = value; } }

    public bool isContactAnything = false;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lr = GetComponent<LineRenderer>();
        _lr.startColor= Color.white;
        _lr.endColor= Color.white;
        _col = _rb.GetComponent<Collider2D>();
    }

    private void OnMouseDown()
    {
        CaculateThrowVector();
        SetArrow();
        PathController.StartVisualizingPath(gameObject);
    }

    private void OnMouseDrag()
    {
        CaculateThrowVector();
        SetArrow();
        PathController.VisualizePath(gameObject, playerVector);
    }

    void CaculateThrowVector()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 distance = mousePos - (Vector2)this.GetComponent<CircleCollider2D>().transform.position;
        playerVector = -distance.normalized * 500;
    }

    void SetArrow()
    {
        _lr.positionCount = 1;
        _lr.SetPosition(0, playerVector.normalized);
        _lr.enabled= true;
    }

    private void OnMouseUp()
    {
        RemoveArrow();
        Throw ();
        PathController.StopVisualizingPath(gameObject);
    }

    void RemoveArrow()
    {
        _lr.enabled= false;
    }

    void Throw()
    {
        _rb.AddForce(playerVector);
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

        //����
        if (gameObject.name == "Player" && GetComponent<Rigidbody2D>().velocity.y < 0 && collision.gameObject.tag != "Ground")
        {
            Managers.Sound.Play("Sound_Landing");
            //GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        //else if (GetComponent<Rigidbody2D>().velocity.y >= 0 && collision.gameObject.tag != "Ground")
        //{
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -GetComponent<Rigidbody2D>().velocity.y);
        //}

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
            _rb.AddForce(dir * 70, ForceMode2D.Force);
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
