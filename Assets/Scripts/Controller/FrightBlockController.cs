using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;

public class FrightBlockController : MonoBehaviour
{
    Vector2 pos;
    int vec = 1;
    float addX = 0.65f;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        sr.flipX = true;
    }

    void Init()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moving();
    }
    void moving()
    {
        pos.x = transform.position.x + addX;
        pos.y = transform.position.y;
        Debug.DrawRay(pos, new Vector3(0.3f * vec, 0, 0), new Color(1, 0, 0));
        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector3(0.3f * vec, 0, 0), 0.3f);

        transform.position = transform.position + new Vector3(1 * vec, 0, 0) * 0.01f;
        if (hit.collider == null)
            return;

        if (hit.collider.tag == "Block" || hit.collider.tag == "InstantiatedWall" || hit.collider.tag == "Wall")
        {
            vec *= -1;
            addX *= -1;

            sr.flipX = sr.flipX ? false : true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player")
            return;

        collision.transform.SetParent(transform, true);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player")
            return;

        //collision.transform.position = collision.transform.position + new Vector3(1 * vec, 0, 0) * 0.01f;
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player")
            return;

        collision.transform.SetParent(null, true);
    }
}
