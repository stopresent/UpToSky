using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlayerController2 : MonoBehaviour
{
    Rigidbody2D _rb;
    LineRenderer _lr;

    Vector2 startPoint;
    Vector2 endPoint;
    float distance;
    Vector2 force;
    Vector2 direction;
    float PushForce = 50f;

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

    public void OnMouseDown()
    {
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //CaculateThrowVector();
        PathController.StartVisualizingPath(gameObject);
    }

    public void OnMouseDrag()
    {
        endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CaculateThrowVector();
        SetArrow();
        PathController.VisualizePath(gameObject, force);
    }

    void CaculateThrowVector()
    {
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force.x = distance * direction.x * PushForce;
        force.y = distance * direction.y * PushForce * 7;
    }

    void SetArrow()
    {
        _lr.positionCount = 1;
        _lr.SetPosition(0, force);
        _lr.enabled= true;
    }

    public void OnMouseUp()
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
        _rb.AddForce(force);
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

        //착지
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
